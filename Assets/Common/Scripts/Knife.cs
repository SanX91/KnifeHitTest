using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class Knife : Attachable
    {
        //private void OnTriggerEnter2D(Collider2D collider)
        //{
        //    if (gameObject.layer.Equals(LayerMask.NameToLayer(Constants.ThrowableLayer)))
        //    {
        //        return;
        //    }

        //    Attachable attachable = collider.GetComponent<Attachable>();
        //    if(attachable == null)
        //    {
        //        return;
        //    }

        //    Rigidbody2D rigidbody = attachable.Rigidbody;
        //    rigidbody.velocity = Vector2.zero;
        //    rigidbody.AddForce(Vector3.down * 20, ForceMode2D.Impulse);
        //    float torque = transform.position.x < rigidbody.transform.position.x ? 50 : -50;
        //    rigidbody.AddTorque(torque,ForceMode2D.Impulse);

        //    StartCoroutine(GameOver());
        //}

    }
}
