using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class LogAttacher : IAttacher
    {
        private List<Attachable> attachedObjects;
        private Transform transform;
        private float logRadius;

        public LogAttacher(Transform transform, float logRadius)
        {
            this.transform = transform;
            this.logRadius = logRadius;

            attachedObjects = new List<Attachable>();
        }

        public void AttachItem(Attachable item)
        {
            attachedObjects.Add(item);
        }

        public void AttachItems(PooledAttachableFactory factory, IEnumerable<int> angles, float upDir = 1)
        {
            foreach (var angle in angles)
            {
                Attachable item = factory.GetEntity();
                item.transform.position = GetPointOnCircle(transform.position, logRadius, angle);
                item.transform.up = (transform.position - item.transform.position).normalized * upDir;
                item.gameObject.SetActive(true);
                attachedObjects.Add(item);
            }
        }

        Vector2 GetPointOnCircle(Vector2 center, float radius, float angle)
        {
            float angleInRad = angle * Mathf.Deg2Rad;
            float x = center.x + radius * Mathf.Cos(angleInRad);
            float y = center.y + radius * Mathf.Sin(angleInRad);

            return new Vector2(x, y);
        }
    }
}
