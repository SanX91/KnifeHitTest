using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChatSystem
{
    public class ChatDatabase
    {
        Dictionary<uint, List<ChatMessage>> messageDictionary;

        public ChatDatabase()
        {
            messageDictionary = new Dictionary<uint, List<ChatMessage>>();
        }

        public void AddEntry(uint friendId, ChatMessage chatMessage)
        {
            if(!messageDictionary.ContainsKey(friendId))
            {
                messageDictionary[friendId] = new List<ChatMessage>();
            }

            messageDictionary[friendId].Add(chatMessage);
            Log();
            EventManager.Instance.TriggerEvent(new ReceiveMessageEvent(chatMessage));
        }

        void Log()
        {
            foreach(var key in messageDictionary.Keys)
            {
                Debug.Log($"{key},{messageDictionary[key].Count}");
            }
        }

        public List<ChatMessage> GetEntries(uint friendId)
        {
            Debug.Log(friendId);
            if (messageDictionary.ContainsKey(friendId))
            {
                return messageDictionary[friendId];
            }

            return null;
        }
    } 

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
