using System;

public class BitcoinPayment : IPaymentStrategy
{
    private string _walletAddress;
    private decimal _currentBitcoinRate;

    public BitcoinPayment(string walletAddress, decimal currentBitcoinRate = 50000m)
    {
        _walletAddress = walletAddress;
        _currentBitcoinRate = currentBitcoinRate;
    }

    public bool ValidatePaymentDetails()
    {
        if (string.IsNullOrWhiteSpace(_walletAddress) || _walletAddress.Length < 26)
            return false;

        if (!_walletAddress.StartsWith("1") && !_walletAddress.StartsWith("3") && !_walletAddress.StartsWith("bc1"))
            return false;

        return true;
    }

    public void Pay(decimal amount)
    {
        if (!ValidatePaymentDetails())
        {
            Console.WriteLine("[ОШИБКА] Неверный адрес Bitcoin кошелька");
            return;
        }

        decimal btcAmount = amount / _currentBitcoinRate;

        Console.WriteLine($"\n₿ ОПЛАТА БИТКОИНОМ");
        Console.WriteLine($"   Кошелек: {_walletAddress}");
        Console.WriteLine($"   Сумма: {amount:C}");
        Console.WriteLine($"   В BTC: {btcAmount:F8} BTC (курс: {_currentBitcoinRate:C})");
        Console.WriteLine($"   Статус: ОПЛАЧЕНО (транзакция в блокчейне)");
    }

    public string GetPaymentMethodName()
    {
        return "Bitcoin";
    }
}