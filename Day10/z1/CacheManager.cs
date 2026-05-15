using System;
using System.Collections.Generic;
using System.Linq;

public class CacheManager
{
    private static CacheManager _instance;
    private static readonly object _lock = new object();
    private Dictionary<string, CacheItem> _cache;
    private int _hitCount;
    private int _missCount;

    private class CacheItem
    {
        public object Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public TimeSpan TimeToLive { get; set; }

        public CacheItem(object value, TimeSpan? timeToLive = null)
        {
            Value = value;
            CreatedAt = DateTime.Now;
            TimeToLive = timeToLive ?? TimeSpan.FromMinutes(10);
        }

        public bool IsExpired => DateTime.Now - CreatedAt > TimeToLive;
    }

    private CacheManager()
    {
        _cache = new Dictionary<string, CacheItem>();
        _hitCount = 0;
        _missCount = 0;
    }

    public static CacheManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CacheManager();
                    }
                }
            }
            return _instance;
        }
    }

    public void AddToCache(string key, object value, TimeSpan? timeToLive = null)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Ключ не может быть пустым");

        lock (_lock)
        {
            CleanExpiredItems();

            if (_cache.ContainsKey(key))
            {
                _cache[key] = new CacheItem(value, timeToLive);
                Logger.Instance.Log($"[КЭШ] Обновлен ключ: {key}");
            }
            else
            {
                _cache.Add(key, new CacheItem(value, timeToLive));
                Logger.Instance.Log($"[КЭШ] Добавлен ключ: {key}");
            }
        }
    }

    public object GetFromCache(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Ключ не может быть пустым");

        lock (_lock)
        {
            CleanExpiredItems();

            if (_cache.ContainsKey(key))
            {
                CacheItem item = _cache[key];

                if (!item.IsExpired)
                {
                    _hitCount++;
                    Logger.Instance.Log($"[КЭШ] HIT: {key}");
                    return item.Value;
                }
                else
                {
                    _cache.Remove(key);
                    Logger.Instance.Log($"[КЭШ] ПРОСРОЧЕН: {key} (удален)");
                }
            }

            _missCount++;
            Logger.Instance.Log($"[КЭШ] MISS: {key}");
            return null;
        }
    }

    public T GetFromCache<T>(string key)
    {
        object value = GetFromCache(key);

        if (value == null)
            return default(T);

        try
        {
            return (T)value;
        }
        catch (InvalidCastException)
        {
            Logger.Instance.Log($"[КЭШ-ОШИБКА] Не удалось привести {key} к типу {typeof(T).Name}");
            return default(T);
        }
    }

    public bool RemoveFromCache(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return false;

        lock (_lock)
        {
            if (_cache.ContainsKey(key))
            {
                _cache.Remove(key);
                Logger.Instance.Log($"[КЭШ] Удален ключ: {key}");
                return true;
            }

            Logger.Instance.Log($"[КЭШ] Не найден для удаления: {key}");
            return false;
        }
    }

    public bool ContainsKey(string key)
    {
        lock (_lock)
        {
            CleanExpiredItems();
            return _cache.ContainsKey(key);
        }
    }

    public void ClearCache()
    {
        lock (_lock)
        {
            int count = _cache.Count;
            _cache.Clear();
            Logger.Instance.Log($"[КЭШ] Полная очистка. Удалено {count} элементов");
        }
    }

    public int GetCacheSize()
    {
        lock (_lock)
        {
            CleanExpiredItems();
            return _cache.Count;
        }
    }

    public void ShowCacheStats()
    {
        lock (_lock)
        {
            Console.WriteLine("\n==================== СТАТИСТИКА КЭША ====================");
            Console.WriteLine($"Количество элементов: {_cache.Count}");
            Console.WriteLine($"HIT (попаданий): {_hitCount}");
            Console.WriteLine($"MISS (промахов): {_missCount}");
            double hitRate = (_hitCount + _missCount) > 0
                ? (double)_hitCount / (_hitCount + _missCount) * 100
                : 0;
            Console.WriteLine($"Точность (Hit Rate): {hitRate:F2}%");
            Console.WriteLine("==========================================================\n");
        }
    }

    public void ShowAllCacheItems()
    {
        lock (_lock)
        {
            CleanExpiredItems();

            if (_cache.Count == 0)
            {
                Console.WriteLine("[КЭШ] Пуст");
                return;
            }

            Console.WriteLine("\n==================== СОДЕРЖИМОЕ КЭША ====================");
            foreach (var kvp in _cache)
            {
                string status = kvp.Value.IsExpired ? "ПРОСРОЧЕН" : "АКТУАЛЕН";
                Console.WriteLine($"  {status}: {kvp.Key} = {kvp.Value.Value} (TTL: {(DateTime.Now - kvp.Value.CreatedAt).TotalSeconds:F1} сек)");
            }
            Console.WriteLine($"Всего: {_cache.Count} элементов");
            Console.WriteLine("==========================================================\n");
        }
    }

    private void CleanExpiredItems()
    {
        List<string> expiredKeys = _cache
            .Where(kvp => kvp.Value.IsExpired)
            .Select(kvp => kvp.Key)
            .ToList();

        foreach (string key in expiredKeys)
        {
            _cache.Remove(key);
            Logger.Instance.Log($"[КЭШ] Автоочистка просроченного: {key}");
        }
    }

    public void RefreshTTL(string key, TimeSpan newTimeToLive)
    {
        lock (_lock)
        {
            if (_cache.ContainsKey(key))
            {
                object value = _cache[key].Value;
                _cache[key] = new CacheItem(value, newTimeToLive);
                Logger.Instance.Log($"[КЭШ] Обновлен TTL для ключа: {key} (+{newTimeToLive.TotalSeconds} сек)");
            }
        }
    }

    public List<string> GetAllKeys()
    {
        lock (_lock)
        {
            CleanExpiredItems();
            return _cache.Keys.ToList();
        }
    }
}