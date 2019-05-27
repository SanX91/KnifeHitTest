using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public abstract class PooledAttachableFactory<T> : MonoBehaviour where T : Attachable
    {
        public T entityPrefab;
        public int poolCount = 10;
        private List<T> pooledEntities;

        private void CreateEntityPool()
        {
            pooledEntities = new List<T>();
            for (int i = 0; i < poolCount; i++)
            {
                pooledEntities.Add(NewElement());
            }
        }

        private T NewElement()
        {
            T element = Instantiate<T>(entityPrefab, transform);
            element.gameObject.SetActive(false);
            return element;
        }

        public T GetAttachable()
        {
            if (pooledEntities == null || pooledEntities.Count == 0)
            {
                CreateEntityPool();
            }

            T element = pooledEntities.Find(x => !x.gameObject.activeSelf);
            if (element == null)
            {
                element = NewElement();
                pooledEntities.Add(element);
            }

            return element;
        }
    } 
}
