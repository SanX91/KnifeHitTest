﻿using System.Collections;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The throwable knife class.
    /// Implements the IThrowable interface.
    /// On collision with another Attachable type of "NonThrowable" layer triggers game over.
    /// </summary>
    public class ThrowableKnife : Attachable, IThrowable
    {
        [SerializeField]
        private new BoxCollider2D collider;
        private bool isGameOver;

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

        /// <summary>
        /// Does a boxcast ahead to check for any collision with another Attachable type of "NonThrowable" layer.
        /// This is done to avoid simultaneous trigger enters on both the log piece and the Attachable type.
        /// On collision with another Attachable type of "NonThrowable" layer triggers game over.
        /// </summary>
        private void FixedUpdate()
        {
            if (isGameOver)
            {
                return;
            }

            //Return if object is attached to the log piece.
            if (gameObject.layer.Equals(LayerMask.NameToLayer(Constants.NonThrowableLayer)))
            {
                return;
            }

            RaycastHit2D[] hits = Physics2D.BoxCastAll(Rigidbody.position + collider.offset, collider.size, 0, Rigidbody.velocity.normalized, 0.5f);
            Transform hitTransform = null;
            bool isMisHit = false;
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    continue;
                }

                if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer(Constants.NonThrowableLayer)))
                {
                    isMisHit = true;
                    hitTransform = hit.transform;
                    break;
                }
            }

            if (!isMisHit)
            {
                return;
            }

            Rigidbody.velocity = Vector2.zero;
            Rigidbody.AddForce(Vector3.down * 20, ForceMode2D.Impulse);
            float torque = transform.position.x < hitTransform.position.x ? 50 : -50;
            Rigidbody.AddTorque(torque, ForceMode2D.Impulse);

            StartCoroutine(GameOver());
        }

        private IEnumerator GameOver()
        {
            isGameOver = true;
            yield return new WaitForSeconds(1);
            EventManager.Instance.TriggerEvent(new GameOverEvent());
        }
    }
}
