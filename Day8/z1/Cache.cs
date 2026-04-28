using System;
using System.Collections;
using System.Collections.Generic;
public class Cache
{
    private Hashtable _cache;
    public Cache()
    {
        _cache = Hashtable.Synchronized(new Hashtable());
    }
    public void AddToCache(string key, object value, TimeSpan? timeToLive = null)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Ключ не может быть пустым");
        }
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Значение не может быть null");
        }
        RemoveExpiredItems();
        var cacheItem = new CacheItem(key, value, timeToLive);
        lock (_cache.SyncRoot)
        {
            if (_cache.ContainsKey(key))
            {
                Console.WriteLine($"[КЭШ] Обновление существующего ключа: {key}");
                _cache[key] = cacheItem;
            }
            else
            {
                Console.WriteLine($"[КЭШ] Добавление нового ключа: {key}");
                _cache.Add(key, cacheItem);
            }
        }
    }
    public object GetFromCache(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Ключ не может быть пустым");
        }
        RemoveExpiredItems();
        lock (_cache.SyncRoot)
        {
            if (_cache.ContainsKey(key))
            {
                var cacheItem = _cache[key] as CacheItem;

                if (cacheItem != null && !cacheItem.IsExpired)
                {
                    Console.WriteLine($"[КЭШ] HIT - данные найдены по ключу: {key}");
                    return cacheItem.Value;
                }
                else
                {
                    _cache.Remove(key);
                    Console.WriteLine($"[КЭШ] MISS - данные просрочены по ключу: {key}");
                    return null;
                }
            }
        }
        Console.WriteLine($"[КЭШ] MISS - данные не найдены по ключу: {key}");
        return null;
    }
    public T GetFromCache<T>(string key)
    {
        var value = GetFromCache(key);
        if (value == null)
        {
            return default(T);
        }
        try
        {
            return (T)value;
        }
        catch (InvalidCastException)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось привести значение к типу {typeof(T).Name}");
            return default(T);
        }
    }
    public bool RemoveFromCache(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Ключ не может быть пустым");
        }
        lock (_cache.SyncRoot)
        {
            if (_cache.ContainsKey(key))
            {
                _cache.Remove(key);
                Console.WriteLine($"[КЭШ] Удален ключ: {key}");
                return true;
            }
        }
        Console.WriteLine($"[КЭШ] Не удалось удалить - ключ не найден: {key}");
        return false;
    }
    public bool ContainsKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return false;
        RemoveExpiredItems();
        lock (_cache.SyncRoot)
        {
            return _cache.ContainsKey(key);
        }
    }
    public void ClearCache()
    {
        lock (_cache.SyncRoot)
        {
            _cache.Clear();
            Console.WriteLine($"[КЭШ] Полная очистка кэша");
        }
    }
    public ICollection GetAllKeys()
    {
        lock (_cache.SyncRoot)
        {
            return _cache.Keys;
        }
    }
    public void PrintAllCacheItems()
    {
        RemoveExpiredItems();
        lock (_cache.SyncRoot)
        {
            if (_cache.Count == 0)
            {
                Console.WriteLine("[КЭШ] Кэш пуст");
                return;
            }
            Console.WriteLine($"\n=== Состояние кэша (всего элементов: {_cache.Count}) ===");
            foreach (DictionaryEntry entry in _cache)
            {
                var cacheItem = entry.Value as CacheItem;
                if (cacheItem != null)
                {
                    string status = cacheItem.IsExpired ? "ПРОСРОЧЕН" : "АКТУАЛЕН";
                    Console.WriteLine($"  {status}: {cacheItem}");
                }
            }
            Console.WriteLine("=========================================\n");
        }
    }
    public int Count
    {
        get
        {
            RemoveExpiredItems();
            lock (_cache.SyncRoot)
            {
                return _cache.Count;
            }
        }
    }
    private void RemoveExpiredItems()
    {
        List<string> expiredKeys = new List<string>();
        lock (_cache.SyncRoot)
        {
            foreach (DictionaryEntry entry in _cache)
            {
                var cacheItem = entry.Value as CacheItem;
                if (cacheItem != null && cacheItem.IsExpired)
                {
                    expiredKeys.Add(entry.Key.ToString());
                }
            }
            foreach (string key in expiredKeys)
            {
                _cache.Remove(key);
                Console.WriteLine($"[КЭШ] Автоудаление просроченного ключа: {key}");
            }
        }
    }
    public void RefreshTTL(string key, TimeSpan newTimeToLive)
    {
        lock (_cache.SyncRoot)
        {
            if (_cache.ContainsKey(key))
            {
                var cacheItem = _cache[key] as CacheItem;
                if (cacheItem != null)
                {
                    cacheItem.TimeToLive = newTimeToLive;
                    cacheItem.CreatedAt = DateTime.Now;
                    Console.WriteLine($"[КЭШ] Обновлен TTL для ключа {key} на {newTimeToLive.TotalMinutes} мин");
                }
            }
        }
    }
}