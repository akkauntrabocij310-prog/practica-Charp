using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string surname = "ivanov";
        string initials = "ii";
        string baseFileName = $"{surname}.{initials}";

        string testDir = @"C:\Temp\FileTest";
        string basePath = Path.Combine(testDir, baseFileName);
        string copyPath = Path.Combine(testDir, $"{surname}_copy.{initials}");
        string movedPath = Path.Combine(testDir, "Subfolder", baseFileName);
        string renamedPath = Path.Combine(testDir, $"{surname}.io");

        Directory.CreateDirectory(testDir);

        FileManager fileManager = new FileManager();
        FileInfoProvider infoProvider = new FileInfoProvider();

        Console.WriteLine("=== ЗАДАНИЕ 1: ОСНОВНЫЕ ОПЕРАЦИИ С ФАЙЛАМИ ===\n");

        Console.WriteLine("--- 1. Создать файл, записать текст, прочитать и вывести ---");
        string initialContent = "Привет, мир! Это тестовый файл.\nСтрока 2: Работа с файлами в C#.\nСтрока 3: " + DateTime.Now;
        fileManager.CreateFile(basePath, initialContent);
        string readContent = fileManager.ReadFile(basePath);
        Console.WriteLine($"Содержимое файла:\n{readContent}\n");
        Console.WriteLine("--- 2. Проверить существование файла перед удалением ---");
        Console.WriteLine($"Файл существует: {fileManager.FileExists(basePath)}");
        fileManager.DeleteFile(basePath);
        Console.WriteLine($"После удаления, файл существует: {fileManager.FileExists(basePath)}");
        fileManager.CreateFile(basePath, initialContent);
        Console.WriteLine("\n--- 3. Получить информацию о файле (размер, даты) ---");
        infoProvider.GetFileInfo(basePath);
        Console.WriteLine("--- 4. Скопировать файл и убедиться, что копия существует ---");
        fileManager.CopyFile(basePath, copyPath);
        Console.WriteLine($"Копия существует: {fileManager.FileExists(copyPath)}");
        infoProvider.GetFileInfo(copyPath);
        Console.WriteLine("--- 5. Переместить файл в новую директорию ---");
        fileManager.MoveFile(copyPath, movedPath);
        Console.WriteLine($"Файл на новом месте существует: {fileManager.FileExists(movedPath)}");
        Console.WriteLine($"Файл по старому пути существует: {fileManager.FileExists(copyPath)}");
        Console.WriteLine("\n--- 6. Переименовать файл в файл familiya.io ---");
        fileManager.RenameFile(movedPath, renamedPath);
        Console.WriteLine($"Файл переименован: {fileManager.FileExists(renamedPath)}");
        infoProvider.GetFileInfo(renamedPath);
        Console.WriteLine("--- 7. Обработать ошибку при удалении несуществующего файла ---");
        fileManager.DeleteFile(@"C:\Temp\NonExistentFile.xyz");
        Console.WriteLine("\n--- 8. Сравнить два файла по размеру ---");
        string anotherFile = Path.Combine(testDir, "another_file.txt");
        fileManager.CreateFile(anotherFile, "Короткий текст");
        infoProvider.CompareFilesBySize(renamedPath, anotherFile);

        Console.WriteLine("--- 9. Удалить все файлы в папке, соответствующие шаблону *.ii ---");
        string patternFile1 = Path.Combine(testDir, "test1.ii");
        string patternFile2 = Path.Combine(testDir, "test2.ii");
        string patternFile3 = Path.Combine(testDir, "test3.txt");
        fileManager.CreateFile(patternFile1, "Тест 1");
        fileManager.CreateFile(patternFile2, "Тест 2");
        fileManager.CreateFile(patternFile3, "Тест 3");
        fileManager.DeleteFilesByPattern(testDir, "*.ii");
        Console.WriteLine("--- 10. Вывести список всех файлов в заданной директории ---");
        fileManager.ListFilesInDirectory(testDir);
        Console.WriteLine("--- 11. Запретить запись в файл и попытаться записать ---");
        string readOnlyFile = Path.Combine(testDir, "readonly.txt");
        fileManager.CreateFile(readOnlyFile, "Начальное содержимое");
        fileManager.SetReadOnly(readOnlyFile, true);
        try
        {
            File.AppendAllText(readOnlyFile, "Попытка записи в read-only файл");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОЖИДАЕМАЯ ОШИБКА] Не удалось записать: {ex.Message}");
        }
        fileManager.SetReadOnly(readOnlyFile, false);
        fileManager.AppendToFile(readOnlyFile, "Теперь запись разрешена!");
        Console.WriteLine($"Содержимое файла: {fileManager.ReadFile(readOnlyFile)}");
        Console.WriteLine("\n--- 12. Проверить доступные права к файлу ---");
        infoProvider.CheckFilePermissions(renamedPath);
        infoProvider.CheckFilePermissions(readOnlyFile);
        Console.WriteLine("\n=== ВСЕ ОПЕРАЦИИ ЗАВЕРШЕНЫ ===");
        Console.WriteLine($"\nРабочая директория: {testDir}");
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}