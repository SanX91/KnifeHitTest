namespace KnifeHitTest
{
    public class GameStartEvent : IEvent
    {
        public object GetData()
        {
            return true;
        }
    }

    public class CurrencyUpdateEvent : IEvent
    {
        int currency;

        public CurrencyUpdateEvent(int currency)
        {
            this.currency = currency;
        }

        public object GetData()
        {
            return currency;
        }
    }

    public class CurrencyPickupEvent : IEvent
    {
        int count;

        public CurrencyPickupEvent(int count)
        {
            this.count = count;
        }

        public object GetData()
        {
            return count;
        }
    }

    public class HighScoreUpdateEvent : IEvent
    {
        int score;

        public HighScoreUpdateEvent(int score)
        {
            this.score = score;
        }

        public object GetData()
        {
            return score;
        }
    }

    public class GameOverEvent : IEvent
    {
        public object GetData()
        {
            return true;
        }
    }
}