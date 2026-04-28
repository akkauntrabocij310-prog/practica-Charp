class Program
{
    static void Main(string[] args)
    {
        BankAccount account = new BankAccount("12345", 1000m);
        Console.WriteLine($"Счет {account.AccountNumber}, баланс: {account.Balance:C}");
        Console.WriteLine();
        TryWithdraw(account, 500m);
        TryWithdraw(account, 800m);
        TryWithdraw(account, 100m);
        TryWithdraw(account, -50m); 
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
    static void TryWithdraw(BankAccount account, decimal amount)
    {
        try
        {
            Console.WriteLine($"Попытка снять: {amount:C}");
            account.Withdraw(amount);
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"Ошибка снятия: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка ввода: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"Текущий баланс: {account.Balance:C}");
            Console.WriteLine("---");
        }
    }
}