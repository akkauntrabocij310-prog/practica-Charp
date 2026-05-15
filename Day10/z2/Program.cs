using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ПАТТЕРН СТРАТЕГИЯ: ОПЛАТА ЗАКАЗА ===\n");

        PaymentProcessor processor = new PaymentProcessor();
        decimal orderAmount = 1500.50m;

        Console.WriteLine($"💰 СУММА ЗАКАЗА: {orderAmount:C}\n");

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nВыберите способ оплаты:");
            Console.WriteLine("  1. Кредитная карта");
            Console.WriteLine("  2. PayPal");
            Console.WriteLine("  3. Bitcoin");
            Console.WriteLine("  4. Наличные (доп. стратегия)");
            Console.WriteLine("  5. Apple Pay (доп. стратегия)");
            Console.WriteLine("  6. Показать историю платежей");
            Console.WriteLine("  7. Оформить возврат");
            Console.WriteLine("  8. Выход");
            Console.Write("\nВаш выбор: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n--- Ввод данных кредитной карты ---");
                    Console.Write("Номер карты (16 цифр): ");
                    string cardNumber = Console.ReadLine();
                    Console.Write("Имя держателя: ");
                    string cardHolder = Console.ReadLine();
                    Console.Write("Срок действия (MM/YY): ");
                    string expiry = Console.ReadLine();
                    Console.Write("CVV (3 цифры): ");
                    string cvv = Console.ReadLine();

                    IPaymentStrategy creditCard = new CreditCardPayment(cardNumber, cardHolder, expiry, cvv);
                    processor.SetPaymentStrategy(creditCard);
                    processor.ProcessPayment(orderAmount);
                    break;

                case "2":
                    Console.WriteLine("\n--- Ввод данных PayPal ---");
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Пароль: ");
                    string password = Console.ReadLine();

                    IPaymentStrategy paypal = new PayPalPayment(email, password);
                    processor.SetPaymentStrategy(paypal);
                    processor.ProcessPayment(orderAmount);
                    break;

                case "3":
                    Console.WriteLine("\n--- Ввод данных Bitcoin ---");
                    Console.Write("Адрес кошелька: ");
                    string wallet = Console.ReadLine();
                    Console.Write("Текущий курс BTC (USD): ");
                    decimal rate = 50000m;
                    if (decimal.TryParse(Console.ReadLine(), out decimal inputRate))
                        rate = inputRate;

                    IPaymentStrategy bitcoin = new BitcoinPayment(wallet, rate);
                    processor.SetPaymentStrategy(bitcoin);
                    processor.ProcessPayment(orderAmount);
                    break;

                case "4":
                    Console.WriteLine("\n--- Оплата наличными ---");
                    Console.Write("Внесенная сумма: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal cashGiven))
                    {
                        IPaymentStrategy cash = new CashPayment(cashGiven);
                        processor.SetPaymentStrategy(cash);
                        processor.ProcessPayment(orderAmount);
                    }
                    else
                    {
                        Console.WriteLine("[ОШИБКА] Неверная сумма");
                    }
                    break;

                case "5":
                    Console.WriteLine("\n--- Оплата Apple Pay ---");
                    Console.Write("ID устройства: ");
                    string deviceId = Console.ReadLine();
                    Console.Write("Отпечаток пальца (для демо): ");
                    string fingerprint = Console.ReadLine();

                    IPaymentStrategy applePay = new ApplePayPayment(deviceId, fingerprint);
                    processor.SetPaymentStrategy(applePay);
                    processor.ProcessPayment(orderAmount);
                    break;

                case "6":
                    processor.ShowPaymentHistory();
                    break;

                case "7":
                    Console.Write("\nСумма возврата: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal refundAmount))
                    {
                        processor.ProcessRefund(refundAmount);
                    }
                    else
                    {
                        Console.WriteLine("[ОШИБКА] Неверная сумма");
                    }
                    break;

                case "8":
                    exit = true;
                    Console.WriteLine("\nСпасибо за использование! До свидания!");
                    break;

                default:
                    Console.WriteLine("[ОШИБКА] Неверный выбор");
                    break;
            }
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}