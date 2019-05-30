namespace ChatSystem
{
    /// <summary>
    /// A chat message class for tracking a message sent by a user with his/her userId.
    /// </summary>
    public class ChatMessage
    {
        public uint UserId { get; private set; }
        public string Message { get; private set; }

        public ChatMessage(uint userId, string message)
        {
            UserId = userId;
            Message = message;
        }
    }
}
