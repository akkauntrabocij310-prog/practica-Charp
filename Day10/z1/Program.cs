using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ПАТТЕРН SINGLETON: ЛОГГЕР И CACHEMANAGER ===\n");

        Console.WriteLine("--- 1. ДЕМОНСТРАЦИЯ SINGLETON LOGGER ---");

        Logger log1 = Logger.Instance;
        Logger log2 = Logger.Instance;

        Console.WriteLine($"log1 и log2 ссылаются на один объект: {ReferenceEquals(log1, log2)}");

        Logger.Instance.Log("Приложение запущено");
        Logger.Instance.Log("Пользователь admin авторизован");
        Logger.Instance.Log("Выполняется загрузка данных");

        Console.WriteLine($"\nКоличество логов: {Logger.Instance.GetLogCount()}");

        Logger.Instance.ShowLogs();

        Console.WriteLine("\n--- 2. ДЕМОНСТРАЦИЯ SINGLETON CACHEMANAGER ---");

        CacheManager cache1 = CacheManager.Instance;
        CacheManager cache2 = CacheManager.Instance;

        Console.WriteLine($"cache1 и cache2 ссылаются на один объект: {ReferenceEquals(cache1, cache2)}");

        CacheManager.Instance.AddToCache("user:1", new { Id = 1, Name = "Иван", Role = "Admin" });
        CacheManager.Instance.AddToCache("user:2", new { Id = 2, Name = "Мария", Role = "User" });
        CacheManager.Instance.AddToCache("config:theme", "Dark");
        CacheManager.Instance.AddToCache("config:language", "ru-RU", TimeSpan.FromSeconds(3));

        CacheManager.Instance.ShowAllCacheItems();

        Console.WriteLine("\n--- 3. ПОЛУЧЕНИЕ ДАННЫХ ИЗ КЭША ---");

        var user1 = CacheManager.Instance.GetFromCache("user:1");
        Console.WriteLine($"user:1 = {user1}");

        var theme = CacheManager.Instance.GetFromCache<string>("config:theme");
        Console.WriteLine($"config:theme = {theme}");

        var notFound = CacheManager.Instance.GetFromCache("non:existent");
        Console.WriteLine($"non:existent = {(notFound == null ? "null" : notFound)}");

        Console.WriteLine("\n--- 4. ОЖИДАНИЕ ИСТЕЧЕНИЯ TTL ---");
        Console.WriteLine("Ожидание 4 секунд для истечения config:language...");

        for (int i = 1; i <= 4; i++)
        {
            Thread.Sleep(1000);
            var lang = CacheManager.Instance.GetFromCache("config:language");
            if (lang == null)
            {
                Console.WriteLine($"  Секунда {i}: config:language ПРОСРОЧЕН и удален");
            }
        }

        CacheManager.Instance.ShowCacheStats();

        Console.WriteLine("\n--- 5. ПРОВЕРКА ЧТО ЛОГГЕР И КЭШ ИСПОЛЬЗУЮТ ОДИН ЭКЗЕМПЛЯР ---");

        CacheManager.Instance.AddToCache("session:token", "abc123xyz");
        CacheManager.Instance.GetFromCache("session:token");
        CacheManager.Instance.RemoveFromCache("user:2");

        Logger.Instance.ShowLogs();

        Console.WriteLine("\n--- 6. ДЕМОНСТРАЦИЯ ПОТОКОБЕЗОПАСНОСТИ ---");

        Thread[] threads = new Thread[5];

        for (int i = 0; i < threads.Length; i++)
        {
            int threadNum = i;
            threads[i] = new Thread(() =>
            {
                for (int j = 0; j < 3; j++)
                {
                    CacheManager.Instance.AddToCache($"thread_{threadNum}_key_{j}", $"value_{threadNum}_{j}");
                    Thread.Sleep(10);
                    var val = CacheManager.Instance.GetFromCache($"thread_{threadNum}_key_{j}");
                    Logger.Instance.Log($"Поток {threadNum}: получено {val}");
                }
            });
            threads[i].Start();
        }

        foreach (var t in threads)
        {
            t.Join();
        }

        Console.WriteLine($"\nИтоговый размер кэша: {CacheManager.Instance.GetCacheSize()}");
        CacheManager.Instance.ShowCacheStats();

        Console.WriteLine("\n--- 7. ОЧИСТКА И ЗАВЕРШЕНИЕ ---");
        CacheManager.Instance.ShowAllCacheItems();
        CacheManager.Instance.ClearCache();
        Logger.Instance.ClearLogs();

        Console.WriteLine($"\nКэш после очистки: {CacheManager.Instance.GetCacheSize()} элементов");
        Console.WriteLine($"Логи после очистки: {Logger.Instance.GetLogCount()} записей");

        Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ ЗАВЕРШЕНА ===");
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}