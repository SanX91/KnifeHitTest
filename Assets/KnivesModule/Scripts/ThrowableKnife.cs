using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class ThrowableKnife : Knife, IThrowable
    {
        [SerializeField]
        new BoxCollider2D collider;

        bool isGameOver;

        protected override void OnEnable()
        {
            base.OnEnable();
            isGameOver = false;
        }

        public void Throw()
        {
            AdjustRigidbodyType(RigidbodyType2D.Dynamic);
            Rigidbody.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
        }

        void FixedUpdate()
        {
            if(isGameOver)
            {
                return;
            }

            if (gameObject.layer.Equals(LayerMask.NameToLayer(Constants.NonThrowableLayer)))
            {
                return;
            }

            RaycastHit2D[] hits = Physics2D.BoxCastAll(Rigidbody.position + collider.offset, collider.size, 0, Rigidbody.velocity.normalized, 0.5f);
            Transform hitTransform = null;
            bool isMisHit = false;
            foreach(var hit in hits)
            {
                if(hit.collider.gameObject == gameObject)
                {
                    continue;
                }

                if(hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer(Constants.NonThrowableLayer)))
                {
                    isMisHit = true;
                    hitTransform = hit.transform;
                    break;
                }
            }

            if(!isMisHit)
            {
                return;
            }

            Rigidbody.velocity = Vector2.zero;
            Rigidbody.AddForce(Vector3.down * 20, ForceMode2D.Impulse);
            float torque = transform.position.x < hitTransform.position.x ? 50 : -50;
            Rigidbody.AddTorque(torque, ForceMode2D.Impulse);

            StartCoroutine(GameOver());
        }

        IEnumerator GameOver()
        {
            isGameOver = true;
            yield return new WaitForSeconds(1);
            EventManager.Instance.TriggerEvent(new GameOverEvent());
        }
    }
}
