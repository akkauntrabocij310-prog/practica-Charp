using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ЗАДАНИЕ 3: ЧТЕНИЕ ДАННЫХ ИЗ ФАЙЛА file.data И ИХ ОБРАБОТКА ===\n");

        string fileName = "file.data";

        Console.WriteLine("--- 1. Создание тестовых данных (если файл пуст) ---");
        CreateTestDataIfNeeded(fileName);

        Console.WriteLine("--- 2. Чтение данных из файла ---");
        LogFileReader reader = new LogFileReader(fileName);
        List<LogEntry> logs = reader.ReadLogs();

        Console.WriteLine($"Прочитано записей: {logs.Count}");
        Console.WriteLine($"Файл существует: {reader.FileExists()}");
        Console.WriteLine($"Всего строк в файле: {reader.GetTotalLines()}");
        Console.WriteLine($"Самая ранняя запись: {reader.GetEarliestLogDate():yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"Самая поздняя запись: {reader.GetLatestLogDate():yyyy-MM-dd HH:mm:ss}");

        Console.WriteLine("\n--- 3. Загрузка данных в LogProcessor ---");
        LogProcessor processor = new LogProcessor();
        processor.LoadLogs(logs);
        processor.PrintStatistics();

        Console.WriteLine("--- 4. Фильтрация по диапазону дат (основное задание) ---");

        DateTime fromDate = new DateTime(2026, 4, 28);
        DateTime toDate = new DateTime(2026, 4, 30);

        Console.WriteLine($"Фильтрация с {fromDate:yyyy-MM-dd} по {toDate:yyyy-MM-dd}");
        var filteredByDate = processor.FilterLogsByDate(fromDate, toDate);
        processor.DisplayLogs(filteredByDate, $"Логи за {fromDate:yyyy-MM-dd} - {toDate:yyyy-MM-dd}");

        Console.WriteLine("--- 5. Фильтрация по конкретной дате ---");
        var todayLogs = processor.FilterByDateOnly(DateTime.Now.Date);
        processor.DisplayLogs(todayLogs, $"Логи за {DateTime.Now.Date:yyyy-MM-dd}");

        Console.WriteLine("--- 6. Фильтрация по месяцу ---");
        var aprilLogs = processor.FilterByMonth(2026, 4);
        processor.DisplayLogs(aprilLogs, "Логи за апрель 2026");

        Console.WriteLine("--- 7. Фильтрация по сообщению ---");
        var errorLogs = processor.FilterLogsByMessage("Ошибка");
        processor.DisplayLogs(errorLogs, "Логи с ошибками");

        var warningLogs = processor.FilterLogsByKeyword("Предупреждение");
        processor.DisplayLogs(warningLogs, "Логи с предупреждениями");

        Console.WriteLine("--- 8. Сортировка ---");

        var sortedAsc = processor.SortByDateAscending();
        processor.DisplayLogs(sortedAsc, "Логи по дате (возрастание)");

        var sortedDesc = processor.SortByDateDescending();
        processor.DisplayLogs(sortedDesc, "Логи по дате (убывание) - первые 10");

        Console.WriteLine("--- 9. Получение последних записей ---");
        var latestLogs = processor.GetLatestLogs(5);
        processor.DisplayLogs(latestLogs, "Последние 5 записей");

        Console.WriteLine("--- 10. Получение первых записей ---");
        var earliestLogs = processor.GetEarliestLogs(5);
        processor.DisplayLogs(earliestLogs, "Первые 5 записей");

        Console.WriteLine("--- 11. Группировка по датам ---");
        var groupedByDate = processor.GroupByDate();
        foreach (var group in groupedByDate)
        {
            Console.WriteLine($"  {group.Key:yyyy-MM-dd}: {group.Value.Count} записей");
        }

        Console.WriteLine("\n--- 12. Чтение с ограничением ---");
        var limitedLogs = reader.ReadLogsWithLimit(3);
        Console.WriteLine($"Первые 3 записи:");
        foreach (var log in limitedLogs)
        {
            Console.WriteLine($"  {log}");
        }

        Console.WriteLine("\n--- 13. Чтение с конца ---");
        var lastLogs = reader.ReadLogsFromEnd(3);
        Console.WriteLine($"Последние 3 записи:");
        foreach (var log in lastLogs)
        {
            Console.WriteLine($"  {log}");
        }

        Console.WriteLine("\n--- 14. Комбинированная фильтрация ---");
        var combinedFilter = processor.FilterLogsByDate(new DateTime(2026, 4, 29), new DateTime(2026, 4, 30));
        combinedFilter = processor.FilterLogsByMessage("Ошибка");
        processor.DisplayLogs(combinedFilter, "Ошибки за 29-30 апреля");

        Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ ЗАВЕРШЕНА ===");
        Console.WriteLine($"\nФайл данных: {System.IO.Path.GetFullPath(fileName)}");
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static void CreateTestDataIfNeeded(string fileName)
    {
        if (System.IO.File.Exists(fileName) && new System.IO.FileInfo(fileName).Length > 0)
        {
            Console.WriteLine($"Файл {fileName} уже существует и содержит данные.");
            return;
        }

        Console.WriteLine($"Создание тестовых данных в файле {fileName}...");

        List<LogEntry> testLogs = new List<LogEntry>
        {
            new LogEntry(new DateTime(2026, 4, 25, 10, 15, 30), "Приложение запущено"),
            new LogEntry(new DateTime(2026, 4, 25, 11, 0, 0), "Информация: Загрузка конфигурации"),
            new LogEntry(new DateTime(2026, 4, 26, 9, 30, 0), "Пользователь admin вошел в систему"),
            new LogEntry(new DateTime(2026, 4, 26, 14, 20, 15), "Ошибка: Не удалось подключиться к БД"),
            new LogEntry(new DateTime(2026, 4, 27, 8, 45, 0), "Предупреждение: Низкий уровень памяти"),
            new LogEntry(new DateTime(2026, 4, 27, 12, 0, 0), "Информация: Отчет сгенерирован"),
            new LogEntry(new DateTime(2026, 4, 28, 10, 0, 0), "Ошибка: Таймаут операции"),
            new LogEntry(new DateTime(2026, 4, 28, 15, 30, 0), "Предупреждение: Высокая нагрузка на CPU"),
            new LogEntry(new DateTime(2026, 4, 29, 9, 0, 0), "Информация: Резервное копирование завершено"),
            new LogEntry(new DateTime(2026, 4, 29, 11, 30, 0), "Ошибка: Недостаточно места на диске"),
            new LogEntry(new DateTime(2026, 4, 29, 16, 45, 0), "Пользователь admin вышел из системы"),
            new LogEntry(new DateTime(2026, 4, 30, 8, 0, 0), "Приложение запущено"),
            new LogEntry(new DateTime(2026, 4, 30, 10, 15, 0), "Информация: Загрузка данных"),
            new LogEntry(new DateTime(2026, 4, 30, 13, 30, 0), "Ошибка: Ошибка валидации данных"),
            new LogEntry(new DateTime(2026, 4, 30, 17, 0, 0), "Информация: Приложение завершает работу"),
        };

        using (var writer = new System.IO.StreamWriter(fileName))
        {
            foreach (var log in testLogs)
            {
                writer.WriteLine(log.ToFileFormat());
            }
        }

        Console.WriteLine($"Создано {testLogs.Count} тестовых записей в {fileName}\n");
    }
}