using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class ThrowableKnife : Attachable, IThrowable
    {
        public void Throw()
        {
            AdjustRigidbodyType(RigidbodyType2D.Dynamic);

            rigidbody.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
        }
    }
}
