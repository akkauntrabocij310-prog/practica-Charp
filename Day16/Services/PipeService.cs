using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using StudentDiaryFull.Models;

namespace StudentDiaryFull.Services
{
    /// <summary>
    /// Чат между студентами и преподавателями через Named Pipes.
    ///
    /// Архитектура: в памяти процесса поддерживается единый список сообщений
    /// (имитация shared-чата). PipeServer слушает входящие сообщения от
    /// PipeClient (второй роли), а PipeClient отправляет исходящие.
    /// </summary>
    public sealed class PipeService
    {
        public static readonly PipeService Instance = new();

        private const string PipeName = "StudentDiaryChat";

        private readonly List<ChatMessage> _messages = new();
        private readonly object            _lock      = new();

        private Thread?                  _serverThread;
        private CancellationTokenSource? _cts;
        private string                   _currentUser = "Система";

        public event Action<ChatMessage>? MessageReceived;
        public IReadOnlyList<ChatMessage> Messages => _messages;

        private PipeService() { }

        // ─── Старт сервера (слушаем входящие) ───────────────────────────────
        public void Start(string userName)
        {
            _currentUser = userName;
            Stop();

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            _serverThread = new Thread(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        using var server = new NamedPipeServerStream(
                            PipeName, PipeDirection.In, 1,
                            PipeTransmissionMode.Byte, PipeOptions.Asynchronous);

                        // Ждём подключения с таймаутом через WaitForConnection + Cancel
                        var connected = false;
                        var ar = server.BeginWaitForConnection(null, null);
                        if (ar.AsyncWaitHandle.WaitOne(500))
                        {
                            server.EndWaitForConnection(ar);
                            connected = true;
                        }

                        if (!connected || token.IsCancellationRequested) continue;

                        using var reader = new StreamReader(server, Encoding.UTF8);
                        var line = reader.ReadLine();
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            var parts = line.Split('|', 2);
                            var msg = new ChatMessage
                            {
                                Sender    = parts.Length > 0 ? parts[0] : "?",
                                Text      = parts.Length > 1 ? parts[1] : line,
                                Timestamp = DateTime.Now
                            };
                            lock (_lock) _messages.Add(msg);
                            MessageReceived?.Invoke(msg);
                        }
                    }
                    catch (OperationCanceledException) { break; }
                    catch { Thread.Sleep(200); }
                }
            })
            { IsBackground = true, Name = "PipeServer" };
            _serverThread.Start();
        }

        // ─── Отправить сообщение ─────────────────────────────────────────────
        public void SendMessage(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            var msg = new ChatMessage
            {
                Sender    = _currentUser,
                Text      = text.Trim(),
                Timestamp = DateTime.Now
            };

            // Сохраняем локально (собственные сообщения)
            lock (_lock) _messages.Add(msg);
            MessageReceived?.Invoke(msg);

            // Пытаемся отправить через Named Pipe
            try
            {
                using var client = new NamedPipeClientStream(".", PipeName,
                    PipeDirection.Out, PipeOptions.None);
                client.Connect(500);
                using var writer = new StreamWriter(client, Encoding.UTF8) { AutoFlush = true };
                writer.WriteLine($"{_currentUser}|{text.Trim()}");
            }
            catch
            {
                // Нет слушателя на другом конце — ничего страшного
            }
        }

        public void Stop()
        {
            _cts?.Cancel();
            _serverThread?.Join(600);
            _cts = null;
            _serverThread = null;
        }
    }
}
