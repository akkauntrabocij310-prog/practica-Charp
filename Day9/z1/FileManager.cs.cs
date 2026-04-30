using System;
using System.IO;
using System.Linq;

public class FileManager
{
    public void CreateFile(string filePath, string content)
    {
        try
        {
            File.WriteAllText(filePath, content);
            Console.WriteLine($"[СОЗДАН] Файл: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось создать файл: {ex.Message}");
        }
    }

    public string ReadFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[ОШИБКА] Файл не существует: {filePath}");
                return null;
            }
            string content = File.ReadAllText(filePath);
            Console.WriteLine($"[ПРОЧИТАН] Файл: {filePath}");
            return content;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось прочитать файл: {ex.Message}");
            return null;
        }
    }

    public void DeleteFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"[УДАЛЕН] Файл: {filePath}");
            }
            else
            {
                Console.WriteLine($"[ПРЕДУПРЕЖДЕНИЕ] Файл не существует: {filePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось удалить файл: {ex.Message}");
        }
    }

    public void CopyFile(string sourcePath, string destPath)
    {
        try
        {
            if (!File.Exists(sourcePath))
            {
                Console.WriteLine($"[ОШИБКА] Исходный файл не существует: {sourcePath}");
                return;
            }

            string destDir = Path.GetDirectoryName(destPath);
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            File.Copy(sourcePath, destPath, true);
            Console.WriteLine($"[СКОПИРОВАН] {sourcePath} -> {destPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось скопировать файл: {ex.Message}");
        }
    }

    public void MoveFile(string sourcePath, string destPath)
    {
        try
        {
            if (!File.Exists(sourcePath))
            {
                Console.WriteLine($"[ОШИБКА] Исходный файл не существует: {sourcePath}");
                return;
            }

            string destDir = Path.GetDirectoryName(destPath);
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            File.Move(sourcePath, destPath);
            Console.WriteLine($"[ПЕРЕМЕЩЕН] {sourcePath} -> {destPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось переместить файл: {ex.Message}");
        }
    }

    public void RenameFile(string oldPath, string newPath)
    {
        MoveFile(oldPath, newPath);
    }

    public bool FileExists(string filePath)
    {
        return File.Exists(filePath);
    }

    public void AppendToFile(string filePath, string content)
    {
        try
        {
            File.AppendAllText(filePath, content);
            Console.WriteLine($"[ДОБАВЛЕНО] В файл {filePath}: {content}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось добавить в файл: {ex.Message}");
        }
    }

    public void DeleteFilesByPattern(string directory, string pattern)
    {
        try
        {
            if (!Directory.Exists(directory))
            {
                Console.WriteLine($"[ОШИБКА] Директория не существует: {directory}");
                return;
            }

            var files = Directory.GetFiles(directory, pattern);
            int deletedCount = 0;

            foreach (var file in files)
            {
                File.Delete(file);
                deletedCount++;
                Console.WriteLine($"[УДАЛЕН ПО ШАБЛОНУ] {file}");
            }

            Console.WriteLine($"[ИТОГ] Удалено {deletedCount} файлов по шаблону '{pattern}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось удалить файлы по шаблону: {ex.Message}");
        }
    }

    public void ListFilesInDirectory(string directory)
    {
        try
        {
            if (!Directory.Exists(directory))
            {
                Console.WriteLine($"[ОШИБКА] Директория не существует: {directory}");
                return;
            }

            var files = Directory.GetFiles(directory);
            Console.WriteLine($"\n=== ФАЙЛЫ В ДИРЕКТОРИИ: {directory} ===");

            if (files.Length == 0)
            {
                Console.WriteLine("  (нет файлов)");
            }
            else
            {
                foreach (var file in files)
                {
                    Console.WriteLine($"  {Path.GetFileName(file)}");
                }
            }
            Console.WriteLine($"Всего: {files.Length} файлов\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось получить список файлов: {ex.Message}");
        }
    }

    public void SetReadOnly(string filePath, bool readOnly)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[ОШИБКА] Файл не существует: {filePath}");
                return;
            }

            File.SetAttributes(filePath, readOnly
                ? FileAttributes.ReadOnly
                : FileAttributes.Normal);

            string status = readOnly ? "запрещена" : "разрешена";
            Console.WriteLine($"[ПРАВА] Запись в файл {filePath} {status}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось изменить права: {ex.Message}");
        }
    }

    public FileAttributes GetFileAttributes(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[ОШИБКА] Файл не существует: {filePath}");
                return FileAttributes.Normal;
            }

            return File.GetAttributes(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось получить атрибуты: {ex.Message}");
            return FileAttributes.Normal;
        }
    }
}