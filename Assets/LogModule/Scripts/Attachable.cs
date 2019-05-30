using System.Collections;
using UnityEngine;

namespace KnifeHitTest
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Attachable : MonoBehaviour
    {
        [SerializeField]
        private new Rigidbody2D rigidbody;
        int initialLayer;

        public Rigidbody2D Rigidbody => rigidbody;

        private void Awake()
        {
            initialLayer = gameObject.layer;
        }

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

        public void AddExplosionForce(float explosionForce, Vector3 explosionPosition)
        {
            if(!gameObject.activeSelf)
            {
                return;
            }

            AdjustRigidbodyType(RigidbodyType2D.Dynamic);
            rigidbody.velocity = Vector2.zero;

            var dir = (transform.position - explosionPosition).normalized;
            dir = (Vector2)dir + Random.insideUnitCircle;
            Rigidbody.AddForce(dir * explosionForce, ForceMode2D.Impulse);
        }
    } 
}
