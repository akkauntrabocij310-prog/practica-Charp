class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Демонстрация обработки ошибки подключения к БД ===\n");
        DatabaseManager dbManager = new DatabaseManager();
        TestConnection(dbManager, "Server=remoteServer;Database=test");
        Console.WriteLine("\n" + new string('-', 50) + "\n");
        TestConnection(dbManager, "");
        Console.WriteLine("\n" + new string('-', 50) + "\n");
        TestConnection(dbManager, "Server=localhost;Database=test");

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
    static void TestConnection(DatabaseManager dbManager, string connectionString)
    {
        try
        {
            dbManager.OpenConnection(connectionString);
            Console.WriteLine("Операция завершена успешно.");
        }
        catch (DatabaseConnectionException ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nПЕРЕХВАЧЕНО В MAIN: {ex.Message}");
            Console.ResetColor();
            if (ex.InnerException != null)
            {
                Console.WriteLine($"  Причина (InnerException): {ex.InnerException.Message}");
            }
            Console.WriteLine("\nПолная информация об исключении:");
            Console.WriteLine(ex.ToString());
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            Console.ResetColor();
        }
        finally
        {
            Console.WriteLine("--- Завершение попытки подключения ---");
        }
    }
}