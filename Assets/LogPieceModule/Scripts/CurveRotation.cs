using System.Collections;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The Curve Rotation class.
    /// Implements the IRotation interface.
    /// Responsible for rotating the log piece with the help of AnimationCurve.
    /// </summary>
    public class CurveRotation : IRotation
    {
        private readonly MonoBehaviour behaviour;
        private readonly Rigidbody2D rigidbody;
        private readonly IRotationSettings settings;
        private Coroutine rotateRoutine;

        public CurveRotation(MonoBehaviour behaviour, Rigidbody2D rigidbody, IRotationSettings settings)
        {
            this.behaviour = behaviour;
            this.rigidbody = rigidbody;
            this.settings = settings;
        }

        /// <summary>
        /// Evaluates the animation curve and retrieves the angle to rotate the rigidbody by.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Rotate()
        {
            float time = 0;

            while (time < 1)
            {
                yield return new WaitForFixedUpdate();
                time += Time.deltaTime / settings.CurveDuration;
                float angle = settings.RotationCurve.Evaluate(time) * settings.AnglePerFrame;
                rigidbody.MoveRotation(rigidbody.rotation + angle);
            }

            rotateRoutine = behaviour.StartCoroutine(Rotate());
        }

        public void ToggleRotate(bool isActive)
        {
            if (isActive)
            {
                rotateRoutine = behaviour.StartCoroutine(Rotate());
                return;
            }

            if (rotateRoutine != null)
            {
                behaviour.StopCoroutine(rotateRoutine);
            }
        }
    }
}
