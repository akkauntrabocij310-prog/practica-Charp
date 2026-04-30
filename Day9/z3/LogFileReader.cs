using System;
using System.Collections.Generic;
using System.IO;

public class LogFileReader
{
    private readonly string _filePath;

    public LogFileReader(string filePath = "file.data")
    {
        _filePath = filePath;
    }

    public string FilePath => _filePath;

    public bool FileExists()
    {
        return File.Exists(_filePath);
    }

    public List<LogEntry> ReadLogs()
    {
        List<LogEntry> entries = new List<LogEntry>();

        if (!FileExists())
        {
            Console.WriteLine($"[ОШИБКА] Файл не существует: {_filePath}");
            return entries;
        }

        try
        {
            string[] lines = File.ReadAllLines(_filePath);
            int lineNumber = 0;

            foreach (string line in lines)
            {
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                LogEntry entry = LogEntry.FromFileFormat(line);

                if (entry != null)
                {
                    entries.Add(entry);
                }
                else
                {
                    Console.WriteLine($"[ПРЕДУПРЕЖДЕНИЕ] Строка {lineNumber} имеет неверный формат: {line}");
                }
            }

            Console.WriteLine($"[ЗАГРУЖЕНО] Прочитано {entries.Count} записей из {_filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось прочитать файл: {ex.Message}");
        }

        return entries;
    }

    public List<LogEntry> ReadLogsWithLimit(int limit)
    {
        List<LogEntry> allEntries = ReadLogs();

        if (allEntries.Count <= limit)
            return allEntries;

        return allEntries.GetRange(0, limit);
    }

    public List<LogEntry> ReadLogsFromEnd(int count)
    {
        List<LogEntry> allEntries = ReadLogs();

        if (allEntries.Count <= count)
            return allEntries;

        return allEntries.GetRange(allEntries.Count - count, count);
    }

    public int GetTotalLines()
    {
        if (!FileExists())
            return 0;

        try
        {
            return File.ReadAllLines(_filePath).Length;
        }
        catch
        {
            return 0;
        }
    }

    public DateTime GetLatestLogDate()
    {
        List<LogEntry> entries = ReadLogs();

        if (entries.Count == 0)
            return DateTime.MinValue;

        DateTime latest = entries[0].Date;
        foreach (var entry in entries)
        {
            if (entry.Date > latest)
                latest = entry.Date;
        }

        return latest;
    }

    public DateTime GetEarliestLogDate()
    {
        List<LogEntry> entries = ReadLogs();

        if (entries.Count == 0)
            return DateTime.MaxValue;

        DateTime earliest = entries[0].Date;
        foreach (var entry in entries)
        {
            if (entry.Date < earliest)
                earliest = entry.Date;
        }

        return earliest;
    }
}