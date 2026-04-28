using System;
public class InsufficientFundsException : Exception
{
    public InsufficientFundsException() : base("Недостаточно средств на счете.")
    {
    }
    public InsufficientFundsException(string message) : base(message)
    {
    }
    public InsufficientFundsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}