using System;
public class DatabaseConnector
{
    public void Connect(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("Строка подключения не может быть пустой.");
        }
        if (!connectionString.Contains("Server=localhost"))
        {
            throw new Exception("SQL Error: Не удалось подключиться к серверу базы данных. " +
                                "Проверьте имя сервера и доступность сети.");
        }
        Console.WriteLine("Подключение к базе данных установлено успешно.");
    }
}