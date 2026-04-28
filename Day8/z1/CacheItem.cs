using System;
public class CacheItem
{
    public string Key { get; set; }
    public object Value { get; set; }
    public DateTime CreatedAt { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public CacheItem(string key, object value, TimeSpan? timeToLive = null)
    {
        Key = key;
        Value = value;
        CreatedAt = DateTime.Now;
        TimeToLive = timeToLive ?? TimeSpan.FromMinutes(10);
    }
    public bool IsExpired
    {
        get { return DateTime.Now - CreatedAt > TimeToLive; }
    }
    public override string ToString()
    {
        return $"[{Key}] = {Value} (создан: {CreatedAt:HH:mm:ss}, TTL: {TimeToLive.TotalMinutes} мин)";
    }
}