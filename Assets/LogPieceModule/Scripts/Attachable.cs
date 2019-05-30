using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The abstract Attachable class.
    /// Anything that can stick to the log piece inherits from this class.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Attachable : MonoBehaviour
    {
        [SerializeField]
        private new Rigidbody2D rigidbody;
        private int initialLayer;

        public Rigidbody2D Rigidbody => rigidbody;

        private void Awake()
        {
            initialLayer = gameObject.layer;
        }

        /// <summary>
        /// Initially sets all the Attachable rigidbody types to kinematic to prevent any unwanted physics simulation.
        /// </summary>
        protected virtual void OnEnable()
        {
            gameObject.layer = initialLayer;
            AdjustRigidbodyType(RigidbodyType2D.Kinematic);
        }

        public void UpdateLayer(string layer)
        {
            gameObject.layer = LayerMask.NameToLayer(layer);
        }

        public void AdjustRigidbodyType(RigidbodyType2D bodyType)
        {
            Rigidbody.bodyType = bodyType;
        }

        /// <summary>
        /// Adds an explosion force to the rigidbody, with a certain amount of randomness.
        /// </summary>
        /// <param name="explosionForce"></param>
        /// <param name="explosionPosition"></param>
        public void AddExplosionForce(float explosionForce, Vector3 explosionPosition)
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            AdjustRigidbodyType(RigidbodyType2D.Dynamic);
            rigidbody.velocity = Vector2.zero;

            Vector3 dir = (transform.position - explosionPosition).normalized;
            dir = (Vector2)dir + Random.insideUnitCircle;
            Rigidbody.AddForce(dir * explosionForce, ForceMode2D.Impulse);
        }
    }
}
