using System;
using System.Collections.Generic;
using System.Linq;

public class LogProcessor
{
    private List<LogEntry> _logs;

    public LogProcessor()
    {
        _logs = new List<LogEntry>();
    }

    public LogProcessor(List<LogEntry> logs)
    {
        _logs = logs ?? new List<LogEntry>();
    }

    public void LoadLogs(List<LogEntry> logs)
    {
        _logs = logs ?? new List<LogEntry>();
        Console.WriteLine($"[ЗАГРУЖЕНО В PROCESSOR] {_logs.Count} записей");
    }

    public List<LogEntry> GetAllLogs()
    {
        return new List<LogEntry>(_logs);
    }

    public List<LogEntry> FilterLogsByDate(DateTime fromDate, DateTime toDate)
    {
        DateTime start = fromDate.Date;
        DateTime end = toDate.Date.AddDays(1).AddTicks(-1);

        var filtered = _logs
            .Where(log => log.Date >= start && log.Date <= end)
            .OrderBy(log => log.Date)
            .ToList();

        Console.WriteLine($"[ФИЛЬТРАЦИЯ ПО ДАТЕ] {fromDate:yyyy-MM-dd} - {toDate:yyyy-MM-dd}: найдено {filtered.Count} записей");

        return filtered;
    }

    public List<LogEntry> FilterLogsByDateRange(DateTime fromDate, DateTime toDate, bool includeTime = false)
    {
        IEnumerable<LogEntry> query = _logs;

        if (includeTime)
        {
            query = query.Where(log => log.Date >= fromDate && log.Date <= toDate);
        }
        else
        {
            DateTime start = fromDate.Date;
            DateTime end = toDate.Date.AddDays(1).AddTicks(-1);
            query = query.Where(log => log.Date >= start && log.Date <= end);
        }

        var filtered = query.OrderBy(log => log.Date).ToList();

        Console.WriteLine($"[ФИЛЬТРАЦИЯ] {fromDate} - {toDate}: найдено {filtered.Count} записей");

        return filtered;
    }

    public List<LogEntry> FilterLogsByMessage(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return new List<LogEntry>(_logs);

        var filtered = _logs
            .Where(log => log.Message.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Console.WriteLine($"[ПОИСК ПО СООБЩЕНИЮ] '{searchText}': найдено {filtered.Count} записей");

        return filtered;
    }

    public List<LogEntry> FilterLogsByMessageExact(string exactMessage)
    {
        if (string.IsNullOrWhiteSpace(exactMessage))
            return new List<LogEntry>(_logs);

        var filtered = _logs
            .Where(log => log.Message.Equals(exactMessage, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Console.WriteLine($"[ПОИСК ТОЧНОГО СОВПАДЕНИЯ] '{exactMessage}': найдено {filtered.Count} записей");

        return filtered;
    }

    public List<LogEntry> FilterLogsByKeyword(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return new List<LogEntry>(_logs);

        var filtered = _logs
            .Where(log => log.Message.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        Console.WriteLine($"[ФИЛЬТРАЦИЯ ПО КЛЮЧЕВОМУ СЛОВУ] '{keyword}': найдено {filtered.Count} записей");

        return filtered;
    }

    public List<LogEntry> SortByDateAscending()
    {
        var sorted = _logs.OrderBy(log => log.Date).ToList();
        Console.WriteLine($"[СОРТИРОВКА] По дате (по возрастанию)");
        return sorted;
    }

    public List<LogEntry> SortByDateDescending()
    {
        var sorted = _logs.OrderByDescending(log => log.Date).ToList();
        Console.WriteLine($"[СОРТИРОВКА] По дате (по убыванию)");
        return sorted;
    }

    public List<LogEntry> SortByMessageAscending()
    {
        var sorted = _logs.OrderBy(log => log.Message).ToList();
        Console.WriteLine($"[СОРТИРОВКА] По сообщению (по возрастанию)");
        return sorted;
    }

    public List<LogEntry> GetLatestLogs(int count)
    {
        var latest = _logs
            .OrderByDescending(log => log.Date)
            .Take(count)
            .ToList();

        Console.WriteLine($"[ПОСЛЕДНИЕ] {count} записей");
        return latest;
    }

    public List<LogEntry> GetEarliestLogs(int count)
    {
        var earliest = _logs
            .OrderBy(log => log.Date)
            .Take(count)
            .ToList();

        Console.WriteLine($"[ПЕРВЫЕ] {count} записей");
        return earliest;
    }

    public List<LogEntry> FilterByDateOnly(DateTime date)
    {
        return FilterLogsByDate(date, date);
    }

    public List<LogEntry> FilterByMonth(int year, int month)
    {
        DateTime start = new DateTime(year, month, 1);
        DateTime end = start.AddMonths(1).AddDays(-1);

        return FilterLogsByDate(start, end);
    }

    public List<LogEntry> FilterByYear(int year)
    {
        DateTime start = new DateTime(year, 1, 1);
        DateTime end = new DateTime(year, 12, 31);

        return FilterLogsByDate(start, end);
    }

    public List<LogEntry> FilterByErrorLevel(string level)
    {
        string searchPattern = $"{level}:";
        return FilterLogsByMessage(searchPattern);
    }

    public Dictionary<DateTime, List<LogEntry>> GroupByDate()
    {
        var grouped = _logs
            .GroupBy(log => log.Date.Date)
            .ToDictionary(g => g.Key, g => g.ToList());

        Console.WriteLine($"[ГРУППИРОВКА ПО ДАТАМ] {grouped.Count} уникальных дат");
        return grouped;
    }

    public Dictionary<string, List<LogEntry>> GroupByMessagePrefix(int prefixLength = 10)
    {
        var grouped = _logs
            .GroupBy(log => log.Message.Length > prefixLength ? log.Message.Substring(0, prefixLength) : log.Message)
            .ToDictionary(g => g.Key, g => g.ToList());

        Console.WriteLine($"[ГРУППИРОВКА ПО ПРЕФИКСУ СООБЩЕНИЯ] {grouped.Count} групп");
        return grouped;
    }

    public void DisplayLogs(List<LogEntry> logs, string title = "Результат")
    {
        if (logs == null || logs.Count == 0)
        {
            Console.WriteLine($"\n=== {title} === НЕТ ЗАПИСЕЙ ===\n");
            return;
        }

        Console.WriteLine($"\n=== {title} ===");
        Console.WriteLine($"Количество записей: {logs.Count}");
        Console.WriteLine("----------------------------------------");

        for (int i = 0; i < logs.Count && i < 50; i++)
        {
            Console.WriteLine($"{i + 1,3}. {logs[i]}");
        }

        if (logs.Count > 50)
        {
            Console.WriteLine($"... и еще {logs.Count - 50} записей");
        }

        Console.WriteLine("=========================================\n");
    }

    public int GetTotalCount()
    {
        return _logs.Count;
    }

    public void PrintStatistics()
    {
        if (_logs.Count == 0)
        {
            Console.WriteLine("[СТАТИСТИКА] Нет данных");
            return;
        }

        var dates = _logs.Select(l => l.Date.Date).Distinct().Count();
        var earliest = _logs.Min(l => l.Date);
        var latest = _logs.Max(l => l.Date);

        var errorCount = _logs.Count(l => l.Message.Contains("Ошибка"));
        var warningCount = _logs.Count(l => l.Message.Contains("Предупреждение"));
        var infoCount = _logs.Count(l => l.Message.Contains("Информация"));

        Console.WriteLine("\n=== СТАТИСТИКА ЛОГОВ ===");
        Console.WriteLine($"Всего записей: {_logs.Count}");
        Console.WriteLine($"Уникальных дат: {dates}");
        Console.WriteLine($"Период: {earliest:yyyy-MM-dd} - {latest:yyyy-MM-dd}");
        Console.WriteLine($"Ошибки: {errorCount}");
        Console.WriteLine($"Предупреждения: {warningCount}");
        Console.WriteLine($"Информация: {infoCount}");
        Console.WriteLine("========================\n");
    }
}