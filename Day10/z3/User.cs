using System;
using System.Collections.Generic;

namespace ChatObserver.Models
{
    /// <summary>
    /// Конкретный подписчик - пользователь чата
    /// </summary>
    public class User : IChatUser
    {
        private string _name;
        private List<string> _receivedMessages;
        private List<DateTime> _messageTimestamps;

        public User(string name)
        {
            _name = name;
            _receivedMessages = new List<string>();
            _messageTimestamps = new List<DateTime>();
        }

        public string GetName() => _name;

        /// <summary>
        /// Обработка полученного сообщения
        /// </summary>
        public void Update(string message)
        {
            _receivedMessages.Add(message);
            _messageTimestamps.Add(DateTime.Now);

            DisplayMessage(message);
            CheckMention(message);
        }

        /// <summary>
        /// Отправка сообщения в чат
        /// </summary>
        public void SendToChat(ChatRoom chat, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine($"[{_name}] Нельзя отправить пустое сообщение");
                return;
            }
            chat.SendMessage(message, this);
        }

        /// <summary>
        /// Отображение полученного сообщения
        /// </summary>
        private void DisplayMessage(string message)
        {
            Console.WriteLine($"  📩 {_name} получил: \"{message}\"");
        }

        /// <summary>
        /// Проверка упоминания пользователя
        /// </summary>
        private void CheckMention(string message)
        {
            if (message.Contains(_name))
            {
                Console.WriteLine($"  ✨ {_name} (упомянут!) реагирует на сообщение");
            }
        }

        /// <summary>
        /// Показать историю сообщений пользователя
        /// </summary>
        public void ShowMessageHistory()
        {
            Console.WriteLine($"\n╔════════════════════════════════════╗");
            Console.WriteLine($"║  История сообщений: {_name,-20} ║");
            Console.WriteLine($"╚════════════════════════════════════╝");

            if (_receivedMessages.Count == 0)
            {
                Console.WriteLine("  Нет сообщений");
                return;
            }

            for (int i = 0; i < _receivedMessages.Count; i++)
            {
                Console.WriteLine($"  [{_messageTimestamps[i]:HH:mm:ss}] {_receivedMessages[i]}");
            }
            Console.WriteLine($"\n  📊 Всего получено: {_receivedMessages.Count} сообщений");
        }

        /// <summary>
        /// Получить статистику пользователя
        /// </summary>
        public void ShowStatistics()
        {
            Console.WriteLine($"\n📊 Статистика {_name}:");
            Console.WriteLine($"  • Получено сообщений: {_receivedMessages.Count}");
            Console.WriteLine($"  • Последнее сообщение: {(_receivedMessages.Count > 0 ? _messageTimestamps[^1].ToString("HH:mm:ss") : "нет")}");
        }
    }
}