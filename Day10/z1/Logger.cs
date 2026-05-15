using System;
using System.Collections.Generic;
using System.Threading;

public class Logger
{
    private static Logger _instance;
    private static readonly object _lock = new object();
    private List<string> _logs;
    private DateTime _startTime;

    private Logger()
    {
        _logs = new List<string>();
        _startTime = DateTime.Now;
        Log("Логгер инициализирован");
    }

    public static Logger Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Logger();
                    }
                }
            }
            return _instance;
        }
    }

    public void Log(string message)
    {
        lock (_lock)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | {message}";
            _logs.Add(logEntry);
            Console.WriteLine($"[LOG] {logEntry}");
        }
    }

    public void ShowLogs()
    {
        lock (_lock)
        {
            Console.WriteLine("\n==================== ВСЕ ЛОГИ ====================");
            Console.WriteLine($"Всего записей: {_logs.Count}");
            Console.WriteLine($"Время работы: {(DateTime.Now - _startTime).TotalSeconds:F2} сек");
            Console.WriteLine("----------------------------------------------------");

            if (_logs.Count == 0)
            {
                Console.WriteLine("  (нет логов)");
            }
            else
            {
                for (int i = 0; i < _logs.Count; i++)
                {
                    Console.WriteLine($"{i + 1,3}. {_logs[i]}");
                }
            }
            Console.WriteLine("====================================================\n");
        }
    }

    public void ClearLogs()
    {
        lock (_lock)
        {
            int count = _logs.Count;
            _logs.Clear();
            Log($"Логи очищены. Удалено {count} записей");
        }
    }

    public int GetLogCount()
    {
        lock (_lock)
        {
            return _logs.Count;
        }
    }

    public List<string> GetRecentLogs(int count)
    {
        lock (_lock)
        {
            int takeCount = Math.Min(count, _logs.Count);
            return _logs.GetRange(_logs.Count - takeCount, takeCount);
        }
    }

    public bool ContainsMessage(string searchText)
    {
        lock (_lock)
        {
            return _logs.Any(log => log.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }
    }
}