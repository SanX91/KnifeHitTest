using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
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
