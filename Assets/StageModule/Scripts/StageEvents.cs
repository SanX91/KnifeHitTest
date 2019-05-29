namespace KnifeHitTest
{
    public class StageStartEvent : IEvent
    {
        IStageSettings stageSettings;

        public StageStartEvent(IStageSettings stageSettings)
        {
            this.stageSettings = stageSettings;
        }

        public object GetData()
        {
            return stageSettings;
        }
    }
}