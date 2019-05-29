﻿namespace KnifeHitTest
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

    public class StageIdEvent : IEvent
    {
        int stageId;

        public StageIdEvent(int stageId)
        {
            this.stageId = stageId;
        }

        public object GetData()
        {
            return stageId;
        }
    }

    public class StageSuccessEvent : IEvent
    {
        public object GetData()
        {
            return true;
        }
    }

    public class StageEndEvent : IEvent
    {
        public object GetData()
        {
            return true;
        }
    }

    public class TurnEndEvent : IEvent
    {
        public object GetData()
        {
            return true;
        }
    }

    public class KnivesUpdateEvent : IEvent
    {
        int knives;

        public KnivesUpdateEvent(int knives)
        {
            this.knives = knives;
        }

        public object GetData()
        {
            return knives;
        }
    }
}