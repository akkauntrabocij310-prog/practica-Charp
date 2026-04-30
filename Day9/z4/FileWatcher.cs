using System;
using System.IO;
using System.Text;
using System.Threading;

public class FileWatcher
{
    private FileSystemWatcher _watcher;
    private string _watchedFolder;
    private string _logFilePath;
    private bool _isWatching;
    private readonly object _logLock = new object();

    public FileWatcher(string folderToWatch, string logFilePath = "log.txt")
    {
        if (!Directory.Exists(folderToWatch))
        {
            Directory.CreateDirectory(folderToWatch);
            Console.WriteLine($"[СОЗДАНА] Папка для отслеживания: {folderToWatch}");
        }

        _watchedFolder = Path.GetFullPath(folderToWatch);
        _logFilePath = Path.GetFullPath(logFilePath);

        InitializeWatcher();
    }

    private void InitializeWatcher()
    {
        _watcher = new FileSystemWatcher
        {
            Path = _watchedFolder,
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.FileName
                          | NotifyFilters.DirectoryName
                          | NotifyFilters.LastWrite
                          | NotifyFilters.CreationTime
                          | NotifyFilters.Size
                          | NotifyFilters.Attributes
        };

        _watcher.Created += OnCreated;
        _watcher.Deleted += OnDeleted;
        _watcher.Changed += OnChanged;
        _watcher.Renamed += OnRenamed;
        _watcher.Error += OnError;
    }

    public void StartWatching()
    {
        if (_isWatching)
        {
            Console.WriteLine("[ПРЕДУПРЕЖДЕНИЕ] Отслеживание уже запущено");
            return;
        }

        try
        {
            _watcher.EnableRaisingEvents = true;
            _isWatching = true;

            LogEvent("СИСТЕМА", "Начало отслеживания", $"Папка: {_watchedFolder}");
            Console.WriteLine($"[ЗАПУЩЕНО] Отслеживание папки: {_watchedFolder}");
            Console.WriteLine($"[ЛОГ-ФАЙЛ] {_logFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось запустить отслеживание: {ex.Message}");
        }
    }

    public void StopWatching()
    {
        if (!_isWatching)
        {
            Console.WriteLine("[ПРЕДУПРЕЖДЕНИЕ] Отслеживание не было запущено");
            return;
        }

        try
        {
            _watcher.EnableRaisingEvents = false;
            _isWatching = false;

            LogEvent("СИСТЕМА", "Остановка отслеживания", $"Папка: {_watchedFolder}");
            Console.WriteLine("[ОСТАНОВЛЕНО] Отслеживание изменений");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось остановить отслеживание: {ex.Message}");
        }
    }

    public bool IsWatching => _isWatching;

    public string WatchedFolder => _watchedFolder;

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        string eventType = e.ChangeType.ToString();
        string fullPath = e.FullPath;
        string fileName = e.Name;

        LogEvent(eventType, fileName, $"Путь: {fullPath}");

        if (File.Exists(fullPath))
        {
            FileInfo info = new FileInfo(fullPath);
            LogEvent(eventType, fileName, $"Размер: {info.Length} байт");
        }

        Console.WriteLine($"[СОЗДАН] {fullPath}");
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        string eventType = e.ChangeType.ToString();
        string fileName = e.Name;
        string fullPath = e.FullPath;

        LogEvent(eventType, fileName, $"Путь: {fullPath}");
        Console.WriteLine($"[УДАЛЕН] {fullPath}");
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        string eventType = e.ChangeType.ToString();
        string fileName = e.Name;
        string fullPath = e.FullPath;

        if (File.Exists(fullPath))
        {
            FileInfo info = new FileInfo(fullPath);
            LogEvent(eventType, fileName, $"Размер: {info.Length} байт | Последнее изменение: {info.LastWriteTime}");
            Console.WriteLine($"[ИЗМЕНЕН] {fullPath}");
        }
        else if (Directory.Exists(fullPath))
        {
            LogEvent(eventType, fileName, $"Директория изменена");
            Console.WriteLine($"[ИЗМЕНЕНА ДИРЕКТОРИЯ] {fullPath}");
        }
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        string eventType = e.ChangeType.ToString();
        string oldName = e.OldName;
        string newName = e.Name;
        string oldPath = e.OldFullPath;
        string newPath = e.FullPath;

        LogEvent(eventType, newName, $"Старое имя: {oldName} | Старый путь: {oldPath} | Новый путь: {newPath}");
        Console.WriteLine($"[ПЕРЕИМЕНОВАН] {oldName} -> {newName}");
    }

    private void OnError(object sender, ErrorEventArgs e)
    {
        Exception exception = e.GetException();
        LogEvent("ОШИБКА", "FileSystemWatcher", exception.Message);
        Console.WriteLine($"[ОШИБКА WATCHER] {exception.Message}");
    }

    private void LogEvent(string eventType, string fileName, string details)
    {
        lock (_logLock)
        {
            try
            {
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | {eventType,-10} | {fileName,-30} | {details}";

                using (StreamWriter writer = new StreamWriter(_logFilePath, append: true, Encoding.UTF8))
                {
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ОШИБКА ЛОГИРОВАНИЯ] {ex.Message}");
            }
        }
    }

    public void ShowLogFile()
    {
        if (!File.Exists(_logFilePath))
        {
            Console.WriteLine($"[ЛОГ-ФАЙЛ НЕ СУЩЕСТВУЕТ] {_logFilePath}");
            return;
        }

        Console.WriteLine($"\n=== СОДЕРЖИМОЕ ЛОГ-ФАЙЛА: {_logFilePath} ===");
        string[] lines = File.ReadAllLines(_logFilePath);

        int linesToShow = Math.Min(30, lines.Length);
        int startLine = Math.Max(0, lines.Length - linesToShow);

        for (int i = startLine; i < lines.Length; i++)
        {
            Console.WriteLine(lines[i]);
        }

        if (lines.Length > 30)
        {
            Console.WriteLine($"... и еще {lines.Length - 30} записей");
        }
        Console.WriteLine($"Всего записей: {lines.Length}");
        Console.WriteLine("=========================================\n");
    }

    public void ClearLogFile()
    {
        try
        {
            File.WriteAllText(_logFilePath, string.Empty, Encoding.UTF8);
            LogEvent("СИСТЕМА", "Лог-файл очищен", "Пользователь");
            Console.WriteLine("[ЛОГ ОЧИЩЕН]");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось очистить лог-файл: {ex.Message}");
        }
    }

    public void GetWatchedFolderInfo()
    {
        Console.WriteLine($"\n=== ИНФОРМАЦИЯ ОБ ОТСЛЕЖИВАЕМОЙ ПАПКЕ ===");
        Console.WriteLine($"Путь: {_watchedFolder}");
        Console.WriteLine($"Отслеживание активно: {_isWatching}");
        Console.WriteLine($"Включает подпапки: {_watcher.IncludeSubdirectories}");

        if (Directory.Exists(_watchedFolder))
        {
            int fileCount = Directory.GetFiles(_watchedFolder, "*", SearchOption.AllDirectories).Length;
            int dirCount = Directory.GetDirectories(_watchedFolder, "*", SearchOption.AllDirectories).Length;
            Console.WriteLine($"Файлов: {fileCount}, Папок: {dirCount}");
        }
        Console.WriteLine("=========================================\n");
    }
}