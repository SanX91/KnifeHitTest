using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The IRotationSettings interface.
    /// </summary>
    public interface IRotationSettings
    {
        float AnglePerFrame { get; }
        AnimationCurve RotationCurve { get; }
        float CurveDuration { get; }
    }
}
