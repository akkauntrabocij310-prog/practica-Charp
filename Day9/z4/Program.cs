using System;
using System.IO;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== FileSystemWatcher – ОТСЛЕЖИВАНИЕ ИЗМЕНЕНИЙ В ФАЙЛАХ ===\n");

        string watchFolder = @"C:\Temp\WatchedFolder";
        string logFile = "log.txt";

        FileWatcher watcher = new FileWatcher(watchFolder, logFile);

        Console.WriteLine("Нажмите:");
        Console.WriteLine("  S - Запустить отслеживание");
        Console.WriteLine("  T - Остановить отслеживание");
        Console.WriteLine("  C - Создать тестовый файл");
        Console.WriteLine("  W - Записать в файл");
        Console.WriteLine("  D - Удалить тестовый файл");
        Console.WriteLine("  R - Переименовать файл");
        Console.WriteLine("  L - Показать лог событий");
        Console.WriteLine("  I - Информация о папке");
        Console.WriteLine("  X - Очистить лог");
        Console.WriteLine("  Q - Выйти");
        Console.WriteLine();

        bool running = true;

        while (running)
        {
            Console.Write("\nВыберите действие: ");
            string key = Console.ReadKey(true).KeyChar.ToString().ToUpper();
            Console.WriteLine();

            switch (key)
            {
                case "S":
                    if (!watcher.IsWatching)
                    {
                        watcher.StartWatching();
                    }
                    else
                    {
                        Console.WriteLine("[ПРЕДУПРЕЖДЕНИЕ] Отслеживание уже запущено");
                    }
                    break;

                case "T":
                    if (watcher.IsWatching)
                    {
                        watcher.StopWatching();
                    }
                    else
                    {
                        Console.WriteLine("[ПРЕДУПРЕЖДЕНИЕ] Отслеживание не запущено");
                    }
                    break;

                case "C":
                    CreateTestFile(watchFolder);
                    break;

                case "W":
                    WriteToFile(watchFolder);
                    break;

                case "D":
                    DeleteTestFile(watchFolder);
                    break;

                case "R":
                    RenameFile(watchFolder);
                    break;

                case "L":
                    watcher.ShowLogFile();
                    break;

                case "I":
                    watcher.GetWatchedFolderInfo();
                    break;

                case "X":
                    watcher.ClearLogFile();
                    break;

                case "Q":
                    if (watcher.IsWatching)
                    {
                        watcher.StopWatching();
                    }
                    running = false;
                    Console.WriteLine("Программа завершена");
                    break;

                default:
                    Console.WriteLine("Неизвестная команда. Попробуйте снова.");
                    break;
            }
        }
    }

    static void CreateTestFile(string folder)
    {
        string fileName = $"test_{DateTime.Now:HHmmss}.txt";
        string fullPath = Path.Combine(folder, fileName);

        try
        {
            File.WriteAllText(fullPath, $"Создан: {DateTime.Now}");
            Console.WriteLine($"[СОЗДАН ТЕСТОВЫЙ ФАЙЛ] {fullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось создать файл: {ex.Message}");
        }
    }

    static void WriteToFile(string folder)
    {
        var files = Directory.GetFiles(folder, "*.txt");

        if (files.Length == 0)
        {
            Console.WriteLine("[ПРЕДУПРЕЖДЕНИЕ] Нет текстовых файлов для записи. Сначала создайте файл (C)");
            return;
        }

        string targetFile = files[0];
        string content = $"Запись добавлена: {DateTime.Now}\n";

        try
        {
            File.AppendAllText(targetFile, content);
            Console.WriteLine($"[ЗАПИСАНО В ФАЙЛ] {targetFile}");
            Console.WriteLine($"  Добавлено: {content.Trim()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось записать в файл: {ex.Message}");
        }
    }

    static void DeleteTestFile(string folder)
    {
        var files = Directory.GetFiles(folder, "*.txt");

        if (files.Length == 0)
        {
            Console.WriteLine("[ПРЕДУПРЕЖДЕНИЕ] Нет файлов для удаления");
            return;
        }

        string targetFile = files[0];

        try
        {
            File.Delete(targetFile);
            Console.WriteLine($"[УДАЛЕН ТЕСТОВЫЙ ФАЙЛ] {targetFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось удалить файл: {ex.Message}");
        }
    }

    static void RenameFile(string folder)
    {
        var files = Directory.GetFiles(folder, "*.txt");

        if (files.Length == 0)
        {
            Console.WriteLine("[ПРЕДУПРЕЖДЕНИЕ] Нет файлов для переименования");
            return;
        }

        string oldPath = files[0];
        string oldName = Path.GetFileName(oldPath);
        string newName = $"renamed_{oldName}";
        string newPath = Path.Combine(folder, newName);

        try
        {
            File.Move(oldPath, newPath);
            Console.WriteLine($"[ПЕРЕИМЕНОВАН] {oldName} -> {newName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось переименовать: {ex.Message}");
        }
    }
}