using System;

public class PayPalPayment : IPaymentStrategy
{
    private string _email;
    private string _password;

    public PayPalPayment(string email, string password)
    {
        _email = email;
        _password = password;
    }

    public bool ValidatePaymentDetails()
    {
        if (string.IsNullOrWhiteSpace(_email) || !_email.Contains("@"))
            return false;

        if (string.IsNullOrWhiteSpace(_password) || _password.Length < 6)
            return false;

        return true;
    }

    public void Pay(decimal amount)
    {
        if (!ValidatePaymentDetails())
        {
            Console.WriteLine("[ОШИБКА] Неверные данные PayPal");
            return;
        }

        Console.WriteLine($"\n🅿️ ОПЛАТА ЧЕРЕЗ PAYPAL");
        Console.WriteLine($"   Аккаунт: {_email}");
        Console.WriteLine($"   Сумма: {amount:C}");
        Console.WriteLine($"   Статус: ОПЛАЧЕНО (перевод на PayPal)");
    }

    public string GetPaymentMethodName()
    {
        return "PayPal";
    }
}