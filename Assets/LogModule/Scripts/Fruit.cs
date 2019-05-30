using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class Fruit : Attachable
    {
        [SerializeField]
        int currenyValue = 1;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            gameObject.SetActive(false);
            EventManager.Instance.TriggerEvent(new CurrencyPickupEvent(currenyValue));
        }
    } 
}
