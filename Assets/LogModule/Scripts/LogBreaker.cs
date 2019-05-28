using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class LogBreaker : IBreaker
    {
        private readonly SpriteRenderer renderer;
        private readonly IEnumerable<Attachable> attachedItems;
        private readonly ILogData logData;

        public LogBreaker(SpriteRenderer renderer, IEnumerable<Attachable> attachedItems, ILogData logData)
        {
            this.renderer = renderer;
            this.attachedItems = attachedItems;
            this.logData = logData;
        }

        public void Break()
        {
            renderer.enabled = false;
            foreach(var item in attachedItems)
            {
                item.transform.SetParent(null);
                item.AddExplosionForce(logData.ExplosionForce, renderer.transform.position, logData.ExplosionRadius);
            }
        }
    }
}
