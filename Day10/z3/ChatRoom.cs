using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatObserver.Models
{
    /// <summary>
    /// Издатель - хранит список подписчиков и уведомляет их об изменениях
    /// </summary>
    public class ChatRoom
    {
        private List<IChatUser> _subscribers;
        private List<string> _messageHistory;
        private readonly int _historyLimit;

        public ChatRoom(int historyLimit = 5)
        {
            _subscribers = new List<IChatUser>();
            _messageHistory = new List<string>();
            _historyLimit = historyLimit;
        }

        /// <summary>
        /// Подписка пользователя на чат
        /// </summary>
        public void Subscribe(IChatUser user)
        {
            if (!_subscribers.Contains(user))
            {
                _subscribers.Add(user);
                OnUserSubscribed(user);
                SendMessageHistory(user);
            }
        }

        /// <summary>
        /// Отписка пользователя от чата
        /// </summary>
        public void Unsubscribe(IChatUser user)
        {
            if (_subscribers.Remove(user))
            {
                OnUserUnsubscribed(user);
            }
        }

        /// <summary>
        /// Отправка сообщения всем подписчикам
        /// </summary>
        public void SendMessage(string message, IChatUser sender)
        {
            string formattedMessage = FormatMessage(message, sender);
            AddToHistory(formattedMessage);
            OnNewMessage(formattedMessage);
            NotifySubscribers(formattedMessage, sender);
        }

        /// <summary>
        /// Уведомление всех подписчиков (кроме отправителя)
        /// </summary>
        private void NotifySubscribers(string message, IChatUser sender)
        {
            foreach (var subscriber in _subscribers.Where(s => s != sender))
            {
                subscriber.Update(message);
            }
        }

        /// <summary>
        /// Отправка истории новому подписчику
        /// </summary>
        private void SendMessageHistory(IChatUser user)
        {
            int startIndex = Math.Max(0, _messageHistory.Count - _historyLimit);
            for (int i = startIndex; i < _messageHistory.Count; i++)
            {
                user.Update($"📜 {_messageHistory[i]}");
            }
        }

        /// <summary>
        /// Форматирование сообщения
        /// </summary>
        private string FormatMessage(string message, IChatUser sender)
        {
            return $"{sender.GetName()}: {message}";
        }

        /// <summary>
        /// Добавление в историю
        /// </summary>
        private void AddToHistory(string message)
        {
            _messageHistory.Add(message);
        }

        /// <summary>
        /// Событие при подписке
        /// </summary>
        private void OnUserSubscribed(IChatUser user)
        {
            Console.WriteLine($"[CHAT] {user.GetName()} подключился к чату");
        }

        /// <summary>
        /// Событие при отписке
        /// </summary>
        private void OnUserUnsubscribed(IChatUser user)
        {
            Console.WriteLine($"[CHAT] {user.GetName()} покинул чат");
        }

        /// <summary>
        /// Событие при новом сообщении
        /// </summary>
        private void OnNewMessage(string message)
        {
            Console.WriteLine($"\n[НОВОЕ СООБЩЕНИЕ] {message}");
        }

        /// <summary>
        /// Получить список активных подписчиков
        /// </summary>
        public List<string> GetActiveSubscribers()
        {
            return _subscribers.Select(s => s.GetName()).ToList();
        }

        /// <summary>
        /// Получить историю сообщений
        /// </summary>
        public IReadOnlyList<string> GetMessageHistory()
        {
            return _messageHistory.AsReadOnly();
        }
    }
}