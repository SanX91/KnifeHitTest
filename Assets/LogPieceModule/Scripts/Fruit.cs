using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The Fruit class.
    /// Is an Attachable type.
    /// Is the in game collectible and currency.
    /// </summary>
    public class Fruit : Attachable
    {
        [SerializeField]
        private int currenyValue = 1;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            gameObject.SetActive(false);
            EventManager.Instance.TriggerEvent(new CurrencyPickupEvent(currenyValue));
        }
    }
}
