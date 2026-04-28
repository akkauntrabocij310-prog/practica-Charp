using System;
using System.Threading;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Хранение кэша данных (Hashtable) ===\n");
        Cache cache = new Cache();
        Console.WriteLine("--- 1. Добавление элементов в кэш ---");
        cache.AddToCache("user:1", new { Id = 1, Name = "Иван", Role = "Admin" });
        cache.AddToCache("user:2", new { Id = 2, Name = "Мария", Role = "User" });
        cache.AddToCache("config:theme", "dark");
        cache.AddToCache("config:language", "ru-RU");
        cache.AddToCache("session:token", Guid.NewGuid().ToString(), TimeSpan.FromSeconds(5));
        cache.PrintAllCacheItems();
        Console.WriteLine("--- 2. Получение элементов из кэша ---");
        var user1 = cache.GetFromCache("user:1");
        Console.WriteLine($"Получен user:1 -> {user1}");
        var theme = cache.GetFromCache<string>("config:theme");
        Console.WriteLine($"Получен config:theme -> {theme}");
        var nonExistent = cache.GetFromCache("non:existent");
        Console.WriteLine($"Получение несуществующего ключа: {(nonExistent == null ? "null" : nonExistent)}");
        Console.WriteLine("\n--- 3. Обобщенное получение с приведением типа ---");
        var language = cache.GetFromCache<string>("config:language");
        Console.WriteLine($"Язык: {language}");
        var wrongCast = cache.GetFromCache<int>("config:theme");
        Console.WriteLine($"Неверное приведение типа: {wrongCast} (default int)");
        Console.WriteLine("\n--- 4. Проверка существования ключей ---");
        Console.WriteLine($"Ключ 'user:1' существует: {cache.ContainsKey("user:1")}");
        Console.WriteLine($"Ключ 'user:999' существует: {cache.ContainsKey("user:999")}");
        Console.WriteLine($"Текущее количество элементов: {cache.Count}");
        Console.WriteLine("\n--- 5. Удаление элементов из кэша ---");
        cache.RemoveFromCache("user:2");
        cache.RemoveFromCache("non:existent");
        cache.PrintAllCacheItems();
        Console.WriteLine("\n--- 6. Автоматическое удаление просроченных элементов ---");
        Console.WriteLine("Ожидание 6 секунд для истечения TTL session:token...");
        for (int i = 1; i <= 6; i++)
        {
            Thread.Sleep(1000);
            var token = cache.GetFromCache("session:token");
            if (token == null && i >= 5)
            {
                Console.WriteLine($"  Секунда {i}: Токен был автоматически удален (просрочен)");
            }
            else if (i < 5)
            {
                Console.WriteLine($"  Секунда {i}: Токен еще активен");
            }
        }
        cache.PrintAllCacheItems();
        Console.WriteLine("\n--- 7. Обновление времени жизни элемента ---");
        cache.AddToCache("temp:data", "Важные данные", TimeSpan.FromSeconds(3));
        Console.WriteLine("Добавлен temp:data с TTL 3 секунды");
        Thread.Sleep(2000);
        cache.RefreshTTL("temp:data", TimeSpan.FromSeconds(5));
        Console.WriteLine("Обновлен TTL temp:data до 5 секунд");
        Thread.Sleep(3000);
        var tempData = cache.GetFromCache("temp:data");
        Console.WriteLine($"temp:data после 3 секунд: {(tempData != null ? "существует" : "удален")}");
        Thread.Sleep(3000);
        tempData = cache.GetFromCache("temp:data");
        Console.WriteLine($"temp:data еще через 3 секунды: {(tempData != null ? "существует" : "удален")}");
        Console.WriteLine("\n--- 8. Получение всех ключей ---");
        var allKeys = cache.GetAllKeys();
        Console.WriteLine("Все ключи в кэше:");
        foreach (var key in allKeys)
        {
            Console.WriteLine($"  - {key}");
        }
        Console.WriteLine("\n--- 9. Полная очистка кэша ---");
        cache.ClearCache();
        cache.PrintAllCacheItems();
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}