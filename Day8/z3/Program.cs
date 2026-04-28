using System;
using System.Collections.Generic;
using System.Linq;
public interface ISettings<T>
{
    void Set(string key, T value);
    T Get(string key);
    bool ContainsKey(string key);
}
public class SettingsStorage<T> : ISettings<T>
{
    protected Dictionary<string, T> _settings;
    public SettingsStorage()
    {
        _settings = new Dictionary<string, T>();
    }
    public void Set(string key, T value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be null or empty");
        if (_settings.ContainsKey(key))
        {
            _settings[key] = value;
        }
        else
        {
            _settings.Add(key, value);
        }
    }
    public T Get(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Key cannot be null or empty");
        if (_settings.ContainsKey(key))
        {
            return _settings[key];
        }
        throw new KeyNotFoundException($"Setting with key '{key}' not found");
    }
    public bool ContainsKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return false;
        return _settings.ContainsKey(key);
    }
    public Dictionary<string, T> GetAllSettings()
    {
        return new Dictionary<string, T>(_settings);
    }
    public bool Remove(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return false;
        return _settings.Remove(key);
    }
    public void Clear()
    {
        _settings.Clear();
    }
    public int Count
    {
        get { return _settings.Count; }
    }
}
public class SettingsManager<T>
{
    private ISettings<T> _settings;
    public SettingsManager(ISettings<T> settings)
    {
        _settings = settings;
    }
    public void SetSetting(string key, T value)
    {
        try
        {
            _settings.Set(key, value);
            Console.WriteLine($"[УСТАНОВЛЕНО] {key} = {value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось установить настройку: {ex.Message}");
        }
    }
    public T GetSetting(string key)
    {
        try
        {
            if (!_settings.ContainsKey(key))
            {
                Console.WriteLine($"[ПРЕДУПРЕЖДЕНИЕ] Настройка '{key}' не найдена");
                return default(T);
            }
            T value = _settings.Get(key);
            Console.WriteLine($"[ПОЛУЧЕНО] {key} = {value}");
            return value;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ОШИБКА] Не удалось получить настройку: {ex.Message}");
            return default(T);
        }
    }
    public void ShowAllSettings()
    {
        if (_settings is SettingsStorage<T> storage)
        {
            var allSettings = storage.GetAllSettings();
            if (allSettings.Count == 0)
            {
                Console.WriteLine("[НАСТРОЙКИ] Нет сохраненных настроек");
                return;
            }
            Console.WriteLine("\n=== ВСЕ НАСТРОЙКИ ===");
            foreach (var kvp in allSettings)
            {
                Console.WriteLine($"  {kvp.Key} : {kvp.Value}");
            }
            Console.WriteLine($"Всего: {allSettings.Count} настроек");
            Console.WriteLine("=====================\n");
        }
        else
        {
            Console.WriteLine("[ОШИБКА] Невозможно отобразить все настройки");
        }
    }
    public void RemoveSetting(string key)
    {
        if (_settings is SettingsStorage<T> storage)
        {
            if (storage.ContainsKey(key))
            {
                storage.Remove(key);
                Console.WriteLine($"[УДАЛЕНО] Настройка '{key}'");
            }
            else
            {
                Console.WriteLine($"[ПРЕДУПРЕЖДЕНИЕ] Настройка '{key}' не найдена для удаления");
            }
        }
        else
        {
            Console.WriteLine("[ОШИБКА] Невозможно удалить настройку");
        }
    }
    public bool SettingExists(string key)
    {
        bool exists = _settings.ContainsKey(key);
        Console.WriteLine($"[ПРОВЕРКА] Настройка '{key}' {(exists ? "существует" : "не существует")}");
        return exists;
    }
    public void UpdateSetting(string key, T newValue)
    {
        if (_settings.ContainsKey(key))
        {
            _settings.Set(key, newValue);
            Console.WriteLine($"[ОБНОВЛЕНО] {key} = {newValue} (было обновлено)");
        }
        else
        {
            Console.WriteLine($"[ПРЕДУПРЕЖДЕНИЕ] Настройка '{key}' не найдена для обновления");
        }
    }
    public void ShowSettingOrDefault(string key, T defaultValue)
    {
        if (_settings.ContainsKey(key))
        {
            T value = _settings.Get(key);
            Console.WriteLine($"[ЗНАЧЕНИЕ] {key} = {value}");
        }
        else
        {
            Console.WriteLine($"[ЗНАЧЕНИЕ ПО УМОЛЧАНИЮ] {key} = {defaultValue}");
        }
    }
    public void BatchSetSettings(Dictionary<string, T> settings)
    {
        Console.WriteLine($"\n[ПАКЕТНАЯ УСТАНОВКА] Добавление {settings.Count} настроек");
        foreach (var kvp in settings)
        {
            SetSetting(kvp.Key, kvp.Value);
        }
    }
    public List<string> SearchKeys(string pattern)
    {
        if (_settings is SettingsStorage<T> storage)
        {
            var allSettings = storage.GetAllSettings();
            var matchedKeys = allSettings.Keys
                .Where(k => k.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                .ToList();
            Console.WriteLine($"[ПОИСК] Ключи, содержащие '{pattern}': найдено {matchedKeys.Count}");
            foreach (var key in matchedKeys)
            {
                Console.WriteLine($"  - {key}");
            }
            return matchedKeys;
        }
        return new List<string>();
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ ISettings<T> И SettingsManager<T> ===\n");
        Console.WriteLine("--- 1. Работа с настройками типа string ---");
        SettingsStorage<string> stringStorage = new SettingsStorage<string>();
        SettingsManager<string> stringManager = new SettingsManager<string>(stringStorage);
        stringManager.SetSetting("AppName", "MyApplication");
        stringManager.SetSetting("Version", "1.0.0");
        stringManager.SetSetting("Theme", "Dark");
        stringManager.SetSetting("Language", "ru-RU");
        stringManager.ShowAllSettings();
        stringManager.GetSetting("Theme");
        stringManager.GetSetting("NonExistentKey");
        stringManager.SettingExists("Version");
        stringManager.SettingExists("Unknown");
        stringManager.UpdateSetting("Version", "2.0.0");
        stringManager.GetSetting("Version");
        stringManager.RemoveSetting("Language");
        stringManager.ShowAllSettings();
        Console.WriteLine("\n--- 2. Работа с настройками типа int ---");
        SettingsStorage<int> intStorage = new SettingsStorage<int>();
        SettingsManager<int> intManager = new SettingsManager<int>(intStorage);
        intManager.SetSetting("MaxConnections", 100);
        intManager.SetSetting("Timeout", 30);
        intManager.SetSetting("RetryCount", 3);
        intManager.SetSetting("Port", 8080);
        intManager.ShowAllSettings();
        int maxConn = intManager.GetSetting("MaxConnections");
        Console.WriteLine($"Получено значение MaxConnections: {maxConn}");
        intManager.ShowSettingOrDefault("BufferSize", 4096);
        intManager.RemoveSetting("RetryCount");
        intManager.ShowAllSettings();
        Console.WriteLine("\n--- 3. Работа с настройками типа bool ---");
        SettingsStorage<bool> boolStorage = new SettingsStorage<bool>();
        SettingsManager<bool> boolManager = new SettingsManager<bool>(boolStorage);
        boolManager.SetSetting("IsEnabled", true);
        boolManager.SetSetting("ShowNotifications", true);
        boolManager.SetSetting("DebugMode", false);
        boolManager.ShowAllSettings();
        boolManager.SettingExists("IsEnabled");
        boolManager.GetSetting("DebugMode");
        Console.WriteLine("\n--- 4. Пакетная установка настроек ---");
        SettingsStorage<double> doubleStorage = new SettingsStorage<double>();
        SettingsManager<double> doubleManager = new SettingsManager<double>(doubleStorage);
        var batchSettings = new Dictionary<string, double>
        {
            { "Temperature", 36.6 },
            { "Pressure", 101.3 },
            { "Humidity", 65.5 },
            { "WindSpeed", 12.8 }
        };
        doubleManager.BatchSetSettings(batchSettings);
        doubleManager.ShowAllSettings();
        Console.WriteLine("\n--- 5. Поиск настроек по шаблону ---");
        doubleManager.SearchKeys("Temp");
        doubleManager.SearchKeys("Speed");
        Console.WriteLine("\n--- 6. Работа с настройками типа List<string> ---");
        SettingsStorage<List<string>> listStorage = new SettingsStorage<List<string>>();
        SettingsManager<List<string>> listManager = new SettingsManager<List<string>>(listStorage);
        var permissions = new List<string> { "Read", "Write", "Execute" };
        listManager.SetSetting("Permissions", permissions);
        var roles = new List<string> { "Admin", "User", "Guest" };
        listManager.SetSetting("Roles", roles);
        listManager.ShowAllSettings();
        var retrievedPermissions = listManager.GetSetting("Permissions");
        if (retrievedPermissions != null)
        {
            Console.WriteLine("Полученные разрешения: " + string.Join(", ", retrievedPermissions));
        }
        Console.WriteLine("\n--- 7. Обработка ошибок ---");
        SettingsStorage<string> errorStorage = new SettingsStorage<string>();
        SettingsManager<string> errorManager = new SettingsManager<string>(errorStorage);
        try
        {
            errorManager.SetSetting("", "InvalidKey");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Перехвачено исключение: {ex.Message}");
        }
        try
        {
            errorManager.GetSetting("NonExistent");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"Перехвачено исключение: {ex.Message}");
        }
        Console.WriteLine("\n=== ДЕМОНСТРАЦИЯ ЗАВЕРШЕНА ===");
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}