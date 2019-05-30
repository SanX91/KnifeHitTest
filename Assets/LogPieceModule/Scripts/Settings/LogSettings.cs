using System;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// Contains all the modifyable settings related to the log piece.
    /// </summary>
    [Serializable]
    public class LogSettings : ILogSettings
    {
        [SerializeField]
        private RotationSettings rotationSettings;
        [SerializeField]
        private List<int> knifeAngles;
        [SerializeField]
        private List<int> fruitAngles;

        public IRotationSettings RotationSettings => rotationSettings;
        public IEnumerable<int> KnifeAngles => knifeAngles;
        public IEnumerable<int> FruitAngles => fruitAngles;
    }
}
