using System;

public class CreditCardPayment : IPaymentStrategy
{
    private string _cardNumber;
    private string _cardHolder;
    private string _expiryDate;
    private string _cvv;

    public CreditCardPayment(string cardNumber, string cardHolder, string expiryDate, string cvv)
    {
        _cardNumber = cardNumber;
        _cardHolder = cardHolder;
        _expiryDate = expiryDate;
        _cvv = cvv;
    }

    public bool ValidatePaymentDetails()
    {
        if (string.IsNullOrWhiteSpace(_cardNumber) || _cardNumber.Length < 13 || _cardNumber.Length > 19)
            return false;

        if (string.IsNullOrWhiteSpace(_cardHolder))
            return false;

        if (string.IsNullOrWhiteSpace(_expiryDate) || !_expiryDate.Contains("/"))
            return false;

        if (string.IsNullOrWhiteSpace(_cvv) || _cvv.Length != 3)
            return false;

        return true;
    }

    public void Pay(decimal amount)
    {
        if (!ValidatePaymentDetails())
        {
            Console.WriteLine("[ОШИБКА] Неверные данные кредитной карты");
            return;
        }

        Console.WriteLine($"\n💳 ОПЛАТА КРЕДИТНОЙ КАРТОЙ");
        Console.WriteLine($"   Карта: ****{_cardNumber.Substring(_cardNumber.Length - 4)}");
        Console.WriteLine($"   Держатель: {_cardHolder}");
        Console.WriteLine($"   Сумма: {amount:C}");
        Console.WriteLine($"   Статус: ОПЛАЧЕНО");
    }

    public string GetPaymentMethodName()
    {
        return "Кредитная карта";
    }
}