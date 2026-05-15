using System;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;

namespace StudentDiaryFull.Services
{
    /// <summary>
    /// Уведомления о новых домашних заданиях через Memory-Mapped Files.
    /// Запись: преподаватель записывает текст уведомления.
    /// Чтение: студент опрашивает MMF и получает новые уведомления.
    /// </summary>
    public sealed class MmfService : IDisposable
    {
        public static readonly MmfService Instance = new();

        private const string MmfName    = "StudentDiaryNotifications";
        private const int    MmfSize    = 4096;  // 4 КБ
        private const int    PollMs     = 2000;

        private MemoryMappedFile? _mmf;
        private CancellationTokenSource? _cts;
        private Thread? _pollThread;
        private bool _disposed;

        public event Action<string>? NotificationReceived;

        private MmfService() { }

        // ─── Запустить опрос (вызвать при входе студента) ────────────────────
        public void StartPolling(string currentUser)
        {
            Stop();
            try
            {
                _mmf = MemoryMappedFile.CreateOrOpen(MmfName, MmfSize);
            }
            catch
            {
                // Если глобальные MMF недоступны — создаём локальный
                _mmf = MemoryMappedFile.CreateNew(null, MmfSize);
            }

            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            _pollThread = new Thread(() =>
            {
                string lastRead = string.Empty;
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        var msg = ReadMessage();
                        if (!string.IsNullOrWhiteSpace(msg) && msg != lastRead)
                        {
                            lastRead = msg;
                            NotificationReceived?.Invoke(msg);
                        }
                    }
                    catch { /* игнорируем ошибки чтения */ }

                    Thread.Sleep(PollMs);
                }
            })
            { IsBackground = true, Name = "MMF-Poller" };
            _pollThread.Start();
        }

        // ─── Записать уведомление (вызывает преподаватель) ──────────────────
        public void WriteNotification(string message)
        {
            EnsureMmf();
            using var accessor = _mmf!.CreateViewAccessor(0, MmfSize);
            var bytes = Encoding.UTF8.GetBytes(message.PadRight(MmfSize / 2, '\0'));
            var len   = Math.Min(bytes.Length, MmfSize - 4);
            accessor.Write(0, len);
            accessor.WriteArray(4, bytes, 0, len);
        }

        // ─── Прочитать текущее уведомление ──────────────────────────────────
        public string ReadMessage()
        {
            EnsureMmf();
            using var accessor = _mmf!.CreateViewAccessor(0, MmfSize);
            int len = accessor.ReadInt32(0);
            if (len <= 0 || len > MmfSize - 4) return string.Empty;
            var bytes = new byte[len];
            accessor.ReadArray(4, bytes, 0, len);
            return Encoding.UTF8.GetString(bytes).TrimEnd('\0');
        }

        public void Stop()
        {
            _cts?.Cancel();
            _pollThread?.Join(500);
            _cts = null;
            _pollThread = null;
        }

        private void EnsureMmf()
        {
            if (_mmf != null) return;
            try
            {
                _mmf = MemoryMappedFile.CreateOrOpen(MmfName, MmfSize);
            }
            catch
            {
                _mmf = MemoryMappedFile.CreateNew(null, MmfSize);
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            Stop();
            _mmf?.Dispose();
            _mmf = null;
        }
    }
}
