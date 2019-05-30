using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The Log Attacher class.
    /// Implements the IAttacher interface.
    /// Responsible for attaching any item or a set of items to the log piece.
    /// </summary>
    public class LogAttacher : IAttacher
    {
        private List<Attachable> attachedItems;
        private GameObject gameObject;
        private readonly float logRadius;

        public LogAttacher(GameObject gameObject, float logRadius)
        {
            this.gameObject = gameObject;
            this.logRadius = logRadius;

            attachedItems = new List<Attachable>();
        }

        public IEnumerable<Attachable> AttachedItems => attachedItems;

        /// <summary>
        /// Attaches an item of type Attachable to the log piece.
        /// Sets the item firmly on a point on the circle.
        /// </summary>
        /// <param name="item"></param>
        public void AttachItem(Attachable item)
        {
            item.transform.SetParent(gameObject.transform);
            item.transform.position = GetPointOnCircle(gameObject.transform.position, logRadius, item.transform.position);
            attachedItems.Add(item);
        }

        /// <summary>
        /// Attach a set of items of type Attachable to the log piece.
        /// Based on the angles attaches the number of items from the factory.
        /// Sets the items firmly on a point on the circle.
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="angles"></param>
        /// <param name="isFlipped">If the art is flipped, this can be set to true.</param>
        public void AttachItems(PooledAttachableFactory factory, IEnumerable<int> angles, bool isFlipped = false)
        {
            foreach (int angle in angles)
            {
                Attachable item = factory.GetEntity();
                item.transform.SetParent(gameObject.transform);
                item.transform.position = GetPointOnCircle(gameObject.transform.position, logRadius, angle);

                Vector3 dir = gameObject.transform.position - item.transform.position;
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

        private Vector2 GetPointOnCircle(Vector2 center, float radius, float angle)
        {
            float angleInRad = angle * Mathf.Deg2Rad;
            float x = center.x + radius * Mathf.Cos(angleInRad);
            float y = center.y + radius * Mathf.Sin(angleInRad);

            return new Vector2(x, y);
        }

        private Vector2 GetPointOnCircle(Vector2 center, float radius, Vector2 point)
        {
            Vector2 dir = point - center;
            return center + (dir.normalized * radius);
        }
    }
}
