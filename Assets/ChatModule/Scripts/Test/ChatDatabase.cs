using System.Collections.Generic;

namespace ChatSystem
{
    /// <summary>
    /// Dummy class which acts as a database for storing all the chat messages.
    /// </summary>
    public class ChatDatabase
    {
        private Dictionary<uint, List<ChatMessage>> messageDictionary;

        public ChatDatabase()
        {
            messageDictionary = new Dictionary<uint, List<ChatMessage>>();
        }

        public void AddEntry(uint friendId, ChatMessage chatMessage)
        {
            if (!messageDictionary.ContainsKey(friendId))
            {
                messageDictionary[friendId] = new List<ChatMessage>();
            }

            messageDictionary[friendId].Add(chatMessage);
            EventManager.Instance.TriggerEvent(new ReceiveMessageEvent(chatMessage));
        }

        public List<ChatMessage> GetEntries(uint friendId)
        {
            if (messageDictionary.ContainsKey(friendId))
            {
                return messageDictionary[friendId];
            }

            return null;
        }
    }
}
