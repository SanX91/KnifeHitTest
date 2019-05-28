using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    [CreateAssetMenu(fileName = "StageSettings", menuName = "Knife Hit Test/StageSettings")]
    public class StageSettings : ScriptableObject, IStageSettings
    {
        [SerializeField]
        private LogSettings logSettings;
        [SerializeField]
        int knives;

        public ILogSettings LogSettings => logSettings;
        public int Knives => knives;
    }
}