using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class LogAttacher : IAttacher
    {
        private List<Attachable> attachedItems;
        private Transform transform;
        private float logRadius;

        public LogAttacher(Transform transform, float logRadius)
        {
            this.transform = transform;
            this.logRadius = logRadius;

            attachedItems = new List<Attachable>();
        }

        public IEnumerable<Attachable> AttachedItems => attachedItems;

        public void AttachItem(Attachable item)
        {
            if(attachedItems.Contains(item))
            {
                return;
            }

            item.transform.SetParent(transform);
            attachedItems.Add(item);
            Debug.Log("Item attached");
        }

        public void AttachItems(PooledAttachableFactory factory, IEnumerable<int> angles, float upDir = 1)
        {
            foreach (var angle in angles)
            {
                Attachable item = factory.GetEntity();
                item.transform.position = GetPointOnCircle(transform.position, logRadius, angle);
                item.transform.up = (transform.position - item.transform.position).normalized * upDir;
                item.gameObject.SetActive(true);
                attachedItems.Add(item);
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
