using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ЗАДАНИЕ 2: ЗАПИСЬ ДАННЫХ В ФАЙЛЫ (БЕЗ СЕРИАЛИЗАЦИИ) ===\n");

        string fileName = "file.data";
        LogFileWriter logWriter = new LogFileWriter(fileName);

        Console.WriteLine($"--- 1. Проверка существования файла до записи ---");
        Console.WriteLine($"Файл '{fileName}' существует: {logWriter.FileExists()}");

        Console.WriteLine("\n--- 2. Добавление первой записи (файл создается) ---");
        LogEntry entry1 = new LogEntry("Приложение запущено");
        logWriter.AppendLogEntry(entry1);

        Console.WriteLine("\n--- 3. Проверка после записи ---");
        Console.WriteLine($"Файл '{fileName}' существует: {logWriter.FileExists()}");
        Console.WriteLine($"Размер файла: {logWriter.GetFileSize()} байт");

        Console.WriteLine("\n--- 4. Добавление второй записи (файл уже существует) ---");
        LogEntry entry2 = new LogEntry("Пользователь авторизован: admin");
        logWriter.AppendLogEntry(entry2);

        Console.WriteLine("\n--- 5. Добавление третьей записи ---");
        LogEntry entry3 = new LogEntry("Выполнен расчет данных");
        logWriter.AppendLogEntry(entry3);

        Console.WriteLine("\n--- 6. Добавление записи с указанной датой ---");
        LogEntry entry4 = new LogEntry(new DateTime(2024, 12, 31, 23, 59, 59), "Последняя запись года");
        logWriter.AppendLogEntry(entry4);

        Console.WriteLine("\n--- 7. Отображение всех записей из файла ---");
        logWriter.DisplayAllEntries();

        Console.WriteLine("\n--- 8. Пакетное добавление нескольких записей ---");
        List<LogEntry> batchEntries = new List<LogEntry>
        {
            new LogEntry("Ошибка: Не удалось подключиться к БД"),
            new LogEntry("Предупреждение: Низкий уровень памяти"),
            new LogEntry("Информация: Отчет сгенерирован"),
            new LogEntry("Ошибка: Таймаут операции")
        };
        logWriter.AppendMultipleEntries(batchEntries);

        Console.WriteLine("\n--- 9. Отображение всех записей после пакетного добавления ---");
        logWriter.DisplayAllEntries();

        Console.WriteLine("\n--- 10. Поиск записей по тексту ---");
        var errorEntries = logWriter.SearchByMessage("Ошибка");
        foreach (var entry in errorEntries)
        {
            Console.WriteLine($"  Найдено: {entry}");
        }

        Console.WriteLine("\n--- 11. Поиск записей по дате ---");
        var todayEntries = logWriter.GetEntriesByDate(DateTime.Now.Date);
        foreach (var entry in todayEntries)
        {
            Console.WriteLine($"  {entry}");
        }

        Console.WriteLine("\n--- 12. Чтение всех записей в список ---");
        List<LogEntry> allEntries = logWriter.ReadAllEntries();
        Console.WriteLine($"Всего записей в файле: {allEntries.Count}");

        Console.WriteLine("\n--- 13. Демонстрация формата файла (сырые данные) ---");
        if (logWriter.FileExists())
        {
            string rawContent = System.IO.File.ReadAllText(fileName);
            Console.WriteLine("Содержимое file.data:");
            Console.WriteLine(rawContent);
        }

        Console.WriteLine("\n--- 14. Дополнительно: проверка при пустом файле ---");
        LogFileWriter newWriter = new LogFileWriter("new_empty_file.data");
        Console.WriteLine($"Новый файл существует: {newWriter.FileExists()}");
        newWriter.DisplayAllEntries();

        LogEntry newEntry = new LogEntry("Первая запись в новом файле");
        newWriter.AppendLogEntry(newEntry);
        newWriter.DisplayAllEntries();

        Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ ЗАВЕРШЕНА ===");
        Console.WriteLine($"\nФайл с данными: {System.IO.Path.GetFullPath(fileName)}");
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}