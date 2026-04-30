using System;
using System.IO;

public class FileInfoProvider
{
    public void GetFileInfo(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[ОШИБКА] Файл не существует: {filePath}");
                return;
            }

            FileInfo fileInfo = new FileInfo(filePath);

            Console.WriteLine($"\n=== ИНФОРМАЦИЯ О ФАЙЛЕ: {Path.GetFileName(filePath)} ===");
            Console.WriteLine($"  Полный путь: {fileInfo.FullName}");
            Console.WriteLine($"  Размер: {fileInfo.Length} байт ({fileInfo.Length / 1024.0:F2} КБ)");
            Console.WriteLine($"  Дата создания: {fileInfo.CreationTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"  Дата последнего изменения: {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"  Дата последнего доступа: {fileInfo.LastAccessTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"  Атрибуты: {fileInfo.Attributes}");
            Console.WriteLine("=========================================\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось получить информацию о файле: {ex.Message}");
        }
    }

    public long GetFileSize(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                return -1;

            return new FileInfo(filePath).Length;
        }
        catch
        {
            return -1;
        }
    }

    public DateTime GetCreationTime(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                return DateTime.MinValue;

            return File.GetCreationTime(filePath);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    public DateTime GetLastWriteTime(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                return DateTime.MinValue;

            return File.GetLastWriteTime(filePath);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    public bool CompareFilesBySize(string filePath1, string filePath2)
    {
        long size1 = GetFileSize(filePath1);
        long size2 = GetFileSize(filePath2);

        if (size1 == -1 || size2 == -1)
        {
            Console.WriteLine("[ОШИБКА] Невозможно сравнить: один из файлов не существует");
            return false;
        }

        bool areEqual = size1 == size2;
        Console.WriteLine($"\n[СРАВНЕНИЕ ФАЙЛОВ ПО РАЗМЕРУ]");
        Console.WriteLine($"  {Path.GetFileName(filePath1)}: {size1} байт");
        Console.WriteLine($"  {Path.GetFileName(filePath2)}: {size2} байт");
        Console.WriteLine($"  Результат: {(areEqual ? "файлы одинакового размера" : "файлы разного размера")}\n");

        return areEqual;
    }

    public void CheckFilePermissions(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[ОШИБКА] Файл не существует: {filePath}");
                return;
            }

            Console.WriteLine($"\n=== ПРАВА ДОСТУПА К ФАЙЛУ: {Path.GetFileName(filePath)} ===");

            bool canRead = true;
            bool canWrite = true;
            bool canExecute = false;

            try
            {
                File.OpenRead(filePath).Close();
                Console.WriteLine("  Чтение: ДОСТУПНО");
            }
            catch
            {
                canRead = false;
                Console.WriteLine("  Чтение: ЗАПРЕЩЕНО");
            }

            try
            {
                using (var fs = File.OpenWrite(filePath))
                {
                    canWrite = true;
                }
                Console.WriteLine("  Запись: ДОСТУПНО");
            }
            catch
            {
                canWrite = false;
                Console.WriteLine("  Запись: ЗАПРЕЩЕНО");
            }

            var attributes = File.GetAttributes(filePath);
            canExecute = attributes.HasFlag(FileAttributes.ReparsePoint);
            Console.WriteLine($"  Выполнение: {(canExecute ? "ДОСТУПНО" : "НЕ ДОСТУПНО (или не применимо)")}");

            Console.WriteLine("=========================================\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось проверить права: {ex.Message}");
        }
    }
}