namespace ChatObserver.Models
{
    public interface IChatUser
    {
        /// <summary>
        /// Получение уведомления о новом сообщении
        /// </summary>
        void Update(string message);

        /// <summary>
        /// Получение имени пользователя
        /// </summary>
        string GetName();

        /// <summary>
        /// Отправка сообщения в чат
        /// </summary>
        void SendToChat(ChatRoom chat, string message);
    }
}