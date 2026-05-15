using System;
using ChatObserver.Models;

namespace ChatObserver
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Чат с паттерном Наблюдатель";

            // Создаем чат (издатель)
            ChatRoom generalChat = new ChatRoom(historyLimit: 5);

            // Создаем пользователей (подписчиков)
            User alice = new User("Алиса");
            User bob = new User("Боб");
            User charlie = new User("Чарли");
            User diana = new User("Диана");

            // Демонстрация работы паттерна
            DemonstrateObserverPattern(generalChat, alice, bob, charlie, diana);

            Console.WriteLine("\n\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void DemonstrateObserverPattern(ChatRoom chat, User alice, User bob, User charlie, User diana)
        {
            PrintHeader("ПАТТЕРН НАБЛЮДАТЕЛЬ - ЧАТ");

            // 1. Подписка пользователей
            PrintPhase("1. ПОДПИСКА ПОЛЬЗОВАТЕЛЕЙ");
            chat.Subscribe(alice);
            chat.Subscribe(bob);
            chat.Subscribe(charlie);

            // 2. Отправка сообщений
            PrintPhase("2. ОТПРАВКА СООБЩЕНИЙ");
            alice.SendToChat(chat, "Всем привет!");
            bob.SendToChat(chat, "Привет, Алиса! Как дела?");
            charlie.SendToChat(chat, "Ребята, отличный чат!");

            // 3. Новый пользователь подписывается и получает историю
            PrintPhase("3. НОВЫЙ ПОЛЬЗОВАТЕЛЬ ПОДПИСЫВАЕТСЯ");
            chat.Subscribe(diana);

            // 4. Продолжение общения
            PrintPhase("4. ПРОДОЛЖЕНИЕ ОБЩЕНИЯ");
            diana.SendToChat(chat, "Присоединяюсь! О чем тут говорят?");
            alice.SendToChat(chat, "Диана, добро пожаловать в наш чат!");

            // 5. Демонстрация отписки
            PrintPhase("5. ОТПИСКА ПОЛЬЗОВАТЕЛЯ");
            chat.Unsubscribe(bob);
            charlie.SendToChat(chat, "Боб куда-то пропал...");

            // 6. Демонстрация упоминания
            PrintPhase("6. УПОМИНАНИЕ ПОЛЬЗОВАТЕЛЯ");
            alice.SendToChat(chat, "Чарли, ты здесь?");

            // 7. Статистика
            PrintPhase("7. СТАТИСТИКА И ИСТОРИЯ");
            alice.ShowStatistics();
            bob.ShowStatistics();
            charlie.ShowStatistics();
            diana.ShowStatistics();

            // 8. Детальная история
            PrintPhase("8. ДЕТАЛЬНАЯ ИСТОРИЯ СООБЩЕНИЙ");
            alice.ShowMessageHistory();
            bob.ShowMessageHistory();
            charlie.ShowMessageHistory();
            diana.ShowMessageHistory();

            // 9. Информация о чате
            PrintPhase("9. ИНФОРМАЦИЯ О ЧАТЕ");
            Console.WriteLine($"\nАктивные пользователи: {string.Join(", ", chat.GetActiveSubscribers())}");
            Console.WriteLine($"Всего сообщений в истории: {chat.GetMessageHistory().Count}");
        }

        static void PrintHeader(string title)
        {
            Console.WriteLine($"\n╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {title,-58} ║");
            Console.WriteLine($"╚══════════════════════════════════════════════════════════╝");
        }

        static void PrintPhase(string phase)
        {
            Console.WriteLine($"\n┌────────────────────────────────────────────────────────┐");
            Console.WriteLine($"│  🔄 {phase,-52} │");
            Console.WriteLine($"└────────────────────────────────────────────────────────┘");
        }
    }
}