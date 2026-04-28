using System;
public class DatabaseConnectionException : Exception
{
    public DatabaseConnectionException() : base("Ошибка подключения к базе данных.")
    {
    }
    public DatabaseConnectionException(string message) : base(message)
    {
    }
    public DatabaseConnectionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}