using System;
namespace NotificationSystem
{
    public delegate void NotificationHandler(string message);
    public class EmailNotifier
    {
        public void SendEmail(string message)
        {
            Console.WriteLine($"[Email] Отправка письма: {message}");
        }
    }
    public class SmsNotifier
    {
        public void SendSms(string message)
        {
            Console.WriteLine($"[SMS] Отправка сообщения: {message}");
        }
    }
}