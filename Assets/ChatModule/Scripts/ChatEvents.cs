using System.Collections.Generic;

namespace ChatSystem
{
    public class ClickFriendEvent : IEvent
    {
        private readonly FriendData friendData;

        public ClickFriendEvent(FriendData data)
        {
            friendData = data;
        }

        public object GetData()
        {
            return friendData;
        }
    }

    public class ExitMessagePanelEvent : IEvent
    {
        public object GetData()
        {
            return true;
        }
    }

    public class SendMessageEvent : IEvent
    {
        private readonly ChatMessage message;

        public SendMessageEvent(ChatMessage message)
        {
            this.message = message;
        }

        public object GetData()
        {
            return message;
        }
    }

    public class ReceiveMessageEvent : IEvent
    {
        private readonly ChatMessage message;

        public ReceiveMessageEvent(ChatMessage message)
        {
            this.message = message;
        }

        public object GetData()
        {
            return message;
        }
    }

    public class FetchConversationEvent : IEvent
    {
        private readonly uint userId;

        public FetchConversationEvent(uint userId)
        {
            this.userId = userId;
        }

        public object GetData()
        {
            return userId;
        }
    }

    public class ReceiveConversationEvent : IEvent
    {
        private readonly List<ChatMessage> messages;

        public ReceiveConversationEvent(List<ChatMessage> messages)
        {
            this.messages = messages;
        }

        public object GetData()
        {
            return messages;
        }
    }
}