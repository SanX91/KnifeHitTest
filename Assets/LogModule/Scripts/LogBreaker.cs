using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class LogBreaker : IBreaker
    {
        private readonly ILogPiece logPiece;
        private readonly IEnumerable<Attachable> attachedItems;
        private readonly ILogData logData;

        public LogBreaker(ILogPiece logPiece, IEnumerable<Attachable> attachedItems, ILogData logData)
        {
            this.logPiece = logPiece;
            this.attachedItems = attachedItems;
            this.logData = logData;
        }

        public void Break()
        {
            logPiece.Renderer.enabled = logPiece.Collider.enabled = false;
            foreach(var item in attachedItems)
            {
                item.transform.SetParent(null);
                item.AddExplosionForce(logData.ExplosionForce, logPiece.Position);
            }
        }

        public void Reset()
        {
            logPiece.Renderer.enabled = logPiece.Collider.enabled = true;
        }
    }
}
