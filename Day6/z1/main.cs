using NotificationSystem;
class Program
{
    static void Main(string[] args)
    {
        EmailNotifier emailService = new EmailNotifier();
        SmsNotifier smsService = new SmsNotifier();
        NotificationHandler handler = new NotificationHandler(emailService.SendEmail);
        handler += smsService.SendSms;
        Console.WriteLine("--- Запуск системы уведомлений ---");
        handler("Ваш заказ успешно оформлен!");
        Console.WriteLine("\n--- Демонстрация удаления метода из делегата ---");
        handler -= smsService.SendSms;
        handler("Внимание: Изменение в графике работы.");
        Console.ReadKey();
    }
}