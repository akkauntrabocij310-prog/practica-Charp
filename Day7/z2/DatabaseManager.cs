using System;
using System.IO;
public class DatabaseManager
{
    private readonly DatabaseConnector _connector = new DatabaseConnector();
    public void OpenConnection(string connectionString)
    {
        try
        {
            Console.WriteLine("Попытка подключения к базе данных...");
            _connector.Connect(connectionString);
        }
        catch (Exception ex)
        {
            LogException(ex);
            throw new DatabaseConnectionException(
                $"Не удалось установить соединение с БД. Строка подключения: {connectionString}",
                ex);
        }
    }
    private void LogException(Exception ex)
    {
        string logMessage = $"""
            ===========================================
            Время: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
            Тип исключения: {ex.GetType().FullName}
            Сообщение: {ex.Message}
            Стек вызовов:
            {ex.StackTrace}
            """;
        if (ex.InnerException != null)
        {
            logMessage += $"""

            ВНУТРЕННЕЕ ИСКЛЮЧЕНИЕ (InnerException):
            Тип: {ex.InnerException.GetType().FullName}
            Сообщение: {ex.InnerException.Message}
            Стек вызовов InnerException:
            {ex.InnerException.StackTrace}
            """;
        }
        logMessage += "\n===========================================\n";
        string logFilePath = "database_errors.log";
        File.AppendAllText(logFilePath, logMessage);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[ЛОГ] Ошибка записана в файл: " + logFilePath);
        Console.WriteLine("[ЛОГ] Содержание ошибки:");
        Console.WriteLine(logMessage);
        Console.ResetColor();
    }
}