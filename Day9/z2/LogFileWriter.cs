using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class LogFileWriter
{
    private readonly string _filePath;

    public LogFileWriter(string filePath = "file.data")
    {
        _filePath = filePath;
    }

    public string FilePath => _filePath;

    public bool FileExists()
    {
        return File.Exists(_filePath);
    }

    public void AppendLogEntry(LogEntry entry)
    {
        if (entry == null)
            throw new ArgumentNullException(nameof(entry), "LogEntry cannot be null");

        try
        {
            bool fileExists = FileExists();
            string line = entry.ToFileFormat();

            using (StreamWriter writer = new StreamWriter(_filePath, append: true))
            {
                writer.WriteLine(line);
            }

            if (fileExists)
            {
                Console.WriteLine($"[ДОБАВЛЕНО] Запись в существующий файл: {_filePath}");
            }
            else
            {
                Console.WriteLine($"[СОЗДАН И ЗАПИСАН] Новый файл: {_filePath}");
            }
            Console.WriteLine($"  Содержание: {entry}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось добавить запись: {ex.Message}");
            throw;
        }
    }

    public void AppendMultipleEntries(List<LogEntry> entries)
    {
        if (entries == null || entries.Count == 0)
        {
            Console.WriteLine("[ПРЕДУПРЕЖДЕНИЕ] Нет записей для добавления");
            return;
        }

        Console.WriteLine($"\n[ПАКЕТНОЕ ДОБАВЛЕНИЕ] {entries.Count} записей");
        foreach (var entry in entries)
        {
            AppendLogEntry(entry);
        }
    }

    public List<LogEntry> ReadAllEntries()
    {
        List<LogEntry> entries = new List<LogEntry>();

        if (!FileExists())
        {
            Console.WriteLine($"[ПРЕДУПРЕЖДЕНИЕ] Файл не существует: {_filePath}");
            return entries;
        }

        try
        {
            string[] lines = File.ReadAllLines(_filePath);
            foreach (string line in lines)
            {
                LogEntry entry = LogEntry.FromFileFormat(line);
                if (entry != null)
                {
                    entries.Add(entry);
                }
            }
            Console.WriteLine($"[ПРОЧИТАНО] {entries.Count} записей из файла: {_filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось прочитать файл: {ex.Message}");
        }

        return entries;
    }

    public void DisplayAllEntries()
    {
        List<LogEntry> entries = ReadAllEntries();

        if (entries.Count == 0)
        {
            Console.WriteLine($"\n=== ФАЙЛ {_filePath} ПУСТ ИЛИ НЕ СУЩЕСТВУЕТ ===\n");
            return;
        }

        Console.WriteLine($"\n=== СОДЕРЖИМОЕ ФАЙЛА: {_filePath} ===");
        Console.WriteLine($"Файл существует: {FileExists()}");
        Console.WriteLine($"Количество записей: {entries.Count}");
        Console.WriteLine("----------------------------------------");

        for (int i = 0; i < entries.Count; i++)
        {
            Console.WriteLine($"{i + 1,3}. {entries[i]}");
        }
        Console.WriteLine("=========================================\n");
    }

    public void ClearFile()
    {
        try
        {
            File.WriteAllText(_filePath, string.Empty);
            Console.WriteLine($"[ОЧИЩЕН] Файл: {_filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось очистить файл: {ex.Message}");
        }
    }

    public void DeleteFile()
    {
        if (FileExists())
        {
            File.Delete(_filePath);
            Console.WriteLine($"[УДАЛЕН] Файл: {_filePath}");
        }
        else
        {
            Console.WriteLine($"[ПРЕДУПРЕЖДЕНИЕ] Файл не существует: {_filePath}");
        }
    }

    public long GetFileSize()
    {
        if (!FileExists())
            return -1;

        return new FileInfo(_filePath).Length;
    }

    public int GetEntriesCount()
    {
        return ReadAllEntries().Count;
    }

    public List<LogEntry> SearchByMessage(string searchText)
    {
        List<LogEntry> allEntries = ReadAllEntries();
        List<LogEntry> results = allEntries
            .Where(e => e.Message.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();

        Console.WriteLine($"[ПОИСК] '{searchText}': найдено {results.Count} записей");
        return results;
    }

    public List<LogEntry> GetEntriesByDate(DateTime date)
    {
        List<LogEntry> allEntries = ReadAllEntries();
        List<LogEntry> results = allEntries
            .Where(e => e.Date.Date == date.Date)
            .ToList();

        Console.WriteLine($"[ПОИСК ПО ДАТЕ] {date:yyyy-MM-dd}: найдено {results.Count} записей");
        return results;
    }
}