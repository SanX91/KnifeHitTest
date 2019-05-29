using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class LogAttacher : IAttacher
    {
        private List<Attachable> attachedItems;
        private GameObject gameObject;
        private float logRadius;

        public LogAttacher(GameObject gameObject, float logRadius)
        {
            this.gameObject = gameObject;
            this.logRadius = logRadius;

            attachedItems = new List<Attachable>();
        }

        public IEnumerable<Attachable> AttachedItems => attachedItems;

        public void AttachItem(Attachable item)
        {
            item.transform.SetParent(gameObject.transform);
            item.transform.position = GetPointOnCircle(gameObject.transform.position, logRadius, item.transform.position);
            attachedItems.Add(item);
            Debug.Log("Item attached");
        }

        public void AttachItems(PooledAttachableFactory factory, IEnumerable<int> angles, bool isFlipped = false)
        {
            foreach (var angle in angles)
            {
                Attachable item = factory.GetEntity();
                item.transform.SetParent(gameObject.transform);
                item.transform.position = GetPointOnCircle(gameObject.transform.position, logRadius, angle);

                var dir = gameObject.transform.position - item.transform.position;
                float lookAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                lookAngle = isFlipped ? lookAngle + 90 : lookAngle - 90;
                item.transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);

                item.gameObject.SetActive(true);
                attachedItems.Add(item);
            }
        }

        public void Reset()
        {
            attachedItems = new List<Attachable>();
        }

        Vector2 GetPointOnCircle(Vector2 center, float radius, float angle)
        {
            float angleInRad = angle * Mathf.Deg2Rad;
            float x = center.x + radius * Mathf.Cos(angleInRad);
            float y = center.y + radius * Mathf.Sin(angleInRad);

            return new Vector2(x, y);
        }
		
		Vector2 GetPointOnCircle(Vector2 center, float radius, Vector2 point)
        {
            Vector2 dir = point - center;
            return center + (dir.normalized * radius);
        }
    }
}
