using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// Contains all stage related settings.
    /// </summary>
    [CreateAssetMenu(fileName = "StageSettings", menuName = "Knife Hit Test/StageSettings")]
    public class StageSettings : ScriptableObject, IStageSettings
    {
        [SerializeField]
        private LogSettings logSettings;
        [SerializeField]
        private int knives = 5;

        public ILogSettings LogSettings => logSettings;
        public int Knives => knives;
    }
}