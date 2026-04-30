using System;

public class LogEntry
{
    public DateTime Date { get; set; }
    public string Message { get; set; }

    public LogEntry()
    {
        Date = DateTime.Now;
        Message = string.Empty;
    }

    public LogEntry(string message)
    {
        Date = DateTime.Now;
        Message = message;
    }

    public LogEntry(DateTime date, string message)
    {
        Date = date;
        Message = message;
    }

    public override string ToString()
    {
        return $"[{Date:yyyy-MM-dd HH:mm:ss}] {Message}";
    }

    public string ToFileFormat()
    {
        return $"{Date:yyyy-MM-dd HH:mm:ss}|{Message}";
    }

    public static LogEntry FromFileFormat(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return null;

        string[] parts = line.Split('|');
        if (parts.Length >= 2 && DateTime.TryParse(parts[0], out DateTime date))
        {
            return new LogEntry(date, parts[1]);
        }
        return null;
    }
}