using UnityEngine;

namespace KnifeHitTest
{
    [CreateAssetMenu(fileName = "RotationSettings", menuName = "Knife Hit Test/RotationSettings")]
    public class RotationSettings : ScriptableObject, IRotationSettings
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
