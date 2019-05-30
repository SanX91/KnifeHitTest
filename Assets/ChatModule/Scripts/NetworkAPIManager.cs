using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChatSystem
{
    public class NetworkAPIManager : MonoBehaviour
    {
        uint userId = 56;
        ChatDatabase chatDatabase;

        private void Start()
        {
            chatDatabase = new ChatDatabase();
        }

        private void OnEnable()
        {
            EventManager.Instance.AddListener<SendMessageEvent>(OnSendMessage);
            EventManager.Instance.AddListener<FetchConversationEvent>(OnFetchConversation);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<SendMessageEvent>(OnSendMessage);
            EventManager.Instance.RemoveListener<FetchConversationEvent>(OnFetchConversation);
        }

        private void OnSendMessage(SendMessageEvent evt)
        {
            ChatMessage message = (ChatMessage)evt.GetData();
            uint friendId = message.UserId;
            message = new ChatMessage(userId, message.Message);
            chatDatabase.AddEntry(friendId, message);
        }

        private void OnFetchConversation(FetchConversationEvent evt)
        {
            EventManager.Instance.TriggerEvent(new ReceiveConversationEvent(chatDatabase.GetEntries((uint)evt.GetData())));
        }
    } 
}
