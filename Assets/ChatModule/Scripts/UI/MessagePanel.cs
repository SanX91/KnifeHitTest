using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ChatSystem
{
    /// <summary>
    /// The Message Panel.
    /// This is the panel in which the user can send a message to one of his/her friends.
    /// If the user receives a message from his/her friend, the same would be displayed in this panel.
    /// Fetches previous conversations with a friend upon returning (Same playthrough, saving/loading not implemented). 
    /// </summary>
    public class MessagePanel : UIPanel
    {
        [SerializeField]
        private PooledMessageUIFactory messageUIFactory;
        [SerializeField]
        private RectTransform content;
        [SerializeField]
        private TextMeshProUGUI headerText;
        [SerializeField]
        private TMP_InputField inputField;
        private FriendData friendData;

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
            if (chatMessages == null)
            {
                return;
            }

            foreach (ChatMessage message in chatMessages)
            {
                CreateMessageUI(message);
            }
        }

        private void OnReceiveMessage(ReceiveMessageEvent evt)
        {
            ChatMessage chatMessage = (ChatMessage)evt.GetData();
            CreateMessageUI(chatMessage);
        }

        private void CreateMessageUI(ChatMessage chatMessage)
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

        private void SetHeader(string text)
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
            if (string.IsNullOrEmpty(inputField.text))
            {
                return;
            }

            EventManager.Instance.TriggerEvent(new SendMessageEvent(new ChatMessage(friendData.UserId, inputField.text)));
            inputField.text = "";
        }
    }
}
