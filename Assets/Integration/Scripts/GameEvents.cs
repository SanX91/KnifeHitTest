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

    public class GameOverEvent : IEvent
    {
        public object GetData()
        {
            return true;
        }
    }
}