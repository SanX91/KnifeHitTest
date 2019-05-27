using System.Collections;
using UnityEngine;

namespace KnifeHitTest
{
    public class CurveRotation : IRotation
    {
        private readonly MonoBehaviour behaviour;
        private readonly IRotationSettings settings;
        private Coroutine rotateRoutine;

        public CurveRotation(MonoBehaviour behaviour, IRotationSettings settings)
        {
            this.behaviour = behaviour;
            this.settings = settings;
        }

        private IEnumerator Rotate()
        {
            float time = 0;

            while (time < 1)
            {
                time += Time.deltaTime / settings.CurveDuration;
                float angle = settings.RotationCurve.Evaluate(time) * settings.AnglePerFrame;
                behaviour.transform.Rotate(Vector3.forward, angle);
                yield return new WaitForEndOfFrame();
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
