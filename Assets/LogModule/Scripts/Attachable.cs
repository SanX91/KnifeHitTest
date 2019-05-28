using UnityEngine;

namespace KnifeHitTest
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Attachable : MonoBehaviour
    {
        [SerializeField]
        protected new Rigidbody2D rigidbody;

        private void Start()
        {
            AdjustRigidbodyType(RigidbodyType2D.Static);
        }

        public void AdjustRigidbodyType(RigidbodyType2D rigidbodyType)
        {
            rigidbody.bodyType = rigidbodyType;
        }

        public void StopMotion()
        {
            AdjustRigidbodyType(RigidbodyType2D.Static);
        }

        public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
        {
            AdjustRigidbodyType(RigidbodyType2D.Dynamic);

            var dir = (rigidbody.transform.position - explosionPosition);
            float wearoff = 1 - (dir.magnitude / explosionRadius);
            rigidbody.AddForce(dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff);
            rigidbody.AddTorque(-dir.x * (wearoff <= 0f ? 0f : explosionForce) * wearoff);
        }
    } 
}
