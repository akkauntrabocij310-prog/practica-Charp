public class BankAccount
{
    public string AccountNumber { get; private set; }
    public decimal Balance { get; private set; }
    public BankAccount(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сумма снятия должна быть больше нуля.");
        }
        if (amount > Balance)
        {
            throw new InsufficientFundsException(
                $"Недостаточно средств. Доступно: {Balance:C}, запрошено: {amount:C}");
        }
        Balance -= amount;
        Console.WriteLine($"Снято {amount:C}. Остаток: {Balance:C}");
    }
}