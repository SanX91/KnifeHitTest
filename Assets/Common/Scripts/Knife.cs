using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class Knife : Attachable
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(!collider.GetComponent<Attachable>())
            {
                return;
            }

            collider.attachedRigidbody.velocity = Vector2.zero;
            collider.attachedRigidbody.AddForce(Vector2.down * 10, ForceMode2D.Impulse);
            collider.attachedRigidbody.AddTorque((transform.position - collider.transform.position).x * 10, ForceMode2D.Impulse);
        }
    }
}
