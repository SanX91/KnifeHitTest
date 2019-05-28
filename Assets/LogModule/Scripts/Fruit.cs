using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class Fruit : Attachable
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.GetComponent<Attachable>())
            {
                return;
            }

            gameObject.SetActive(false);
        }
    } 
}
