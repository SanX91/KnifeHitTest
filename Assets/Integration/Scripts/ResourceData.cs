using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    [Serializable]
    public class ResourceData
    {
        [SerializeField]
        int currency;

        public int Currency => currency;

        public void AdjustResource(int amount, bool isExpense = false)
        {
            if(isExpense)
            {
                currency -= amount;
                return;
            }

            currency += amount;
        }
    } 
}
