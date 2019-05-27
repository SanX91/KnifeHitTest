using UnityEngine;

namespace KnifeHitTest
{
    public interface IRotationSettings
    {
        float AnglePerFrame { get; }
        AnimationCurve RotationCurve { get; }
        float CurveDuration { get; }
    } 
}
