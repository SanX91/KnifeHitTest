using System;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// Contains the log piece rotation settings. 
    /// </summary>
    [Serializable]
    public class RotationSettings : IRotationSettings
    {
        [SerializeField]
        private float speed, curveDuration;
        [SerializeField]
        private AnimationCurve rotationCurve;

        public float AnglePerFrame => speed;
        public AnimationCurve RotationCurve => rotationCurve;
        public float CurveDuration => curveDuration;
    }
}
