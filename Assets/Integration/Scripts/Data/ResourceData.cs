using System;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The resource data class.
    /// Stores the amount of fruits (in game currency) collected in the game.
    /// Has method to spend or add currency.
    /// </summary>
    [Serializable]
    public class ResourceData
    {
        [SerializeField]
        private int currency;

        public int Currency => currency;

        /// <summary>
        /// Can add further resources.
        /// Can remove resources.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="isExpense"></param>
        public void AdjustResource(int amount, bool isExpense = false)
        {
            if (isExpense)
            {
                currency -= amount;
                return;
            }

            currency += amount;
        }
    }
}
