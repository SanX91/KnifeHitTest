using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace ChatSystem
{
    public class MessagePanel : UIPanel
    {
        [SerializeField]
        PooledMessageUIFactory messageUIFactory;
        [SerializeField]
        RectTransform content;
        [SerializeField]
        TextMeshProUGUI headerText;
        [SerializeField]
        TMP_InputField inputField;

        FriendData friendData;

        private void OnEnable()
        {
            EventManager.Instance.AddListener<ReceiveMessageEvent>(OnReceiveMessage);
            EventManager.Instance.AddListener<ReceiveConversationEvent>(OnReceiveConversation);
        }

        private void OnDisable()
        {
            EventManager.Instance.RemoveListener<ReceiveMessageEvent>(OnReceiveMessage);
            EventManager.Instance.RemoveListener<ReceiveConversationEvent>(OnReceiveConversation);
        }

        private void OnReceiveConversation(ReceiveConversationEvent evt)
        {
            List<ChatMessage> chatMessages = (List<ChatMessage>)evt.GetData();
            if(chatMessages == null)
            {
                Debug.Log("NULL");
                return;
            }

            Debug.Log("NOT NULL");
            foreach (var message in chatMessages)
            {
                CreateMessageUI(message);
            }
        }

        private void OnReceiveMessage(ReceiveMessageEvent evt)
        {
            ChatMessage chatMessage = (ChatMessage)evt.GetData();
            CreateMessageUI(chatMessage);
        }

        void CreateMessageUI(ChatMessage chatMessage)
        {
            MessageUI messageUI = messageUIFactory.GetEntity();
            messageUI.transform.SetParent(content);
            messageUI.gameObject.SetActive(true);

            messageUI.SetText(chatMessage.Message, !chatMessage.UserId.Equals(friendData.UserId));
        }

        public override void Open(object param = null)
        {
            base.Open(param);
            friendData = (FriendData)param;
            SetHeader(friendData.Name);

            EventManager.Instance.TriggerEvent(new FetchConversationEvent(friendData.UserId));
            inputField.Select();
        }

        void SetHeader(string text)
        {
            headerText.SetText(text);
        }

        public void OnBack()
        {
            messageUIFactory.Reset();

            EventManager.Instance.TriggerEvent(new ExitMessagePanelEvent());
        }

        public void OnSendMessage()
        {
            if(string.IsNullOrEmpty(inputField.text))
            {
                return;
            }

            Debug.Log(inputField.text);
            EventManager.Instance.TriggerEvent(new SendMessageEvent(new ChatMessage(friendData.UserId,inputField.text)));
            inputField.text = "";
        }
    } 
}
