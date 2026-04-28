using System;
public class EmptyStringException : Exception
{
    public EmptyStringException()
        : base("Строка не может быть пустой или null.")
    {
    }
    public EmptyStringException(string message)
        : base(message)
    {
    }
    public EmptyStringException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}