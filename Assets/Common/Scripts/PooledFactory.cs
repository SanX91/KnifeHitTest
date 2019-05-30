using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public abstract class PooledFactory<T> : MonoBehaviour where T : Component
    {
        public T entityPrefab;
        public int poolCount = 10;
        private List<T> pooledEntities;

        private void Start()
        {
            CreateEntityPool();
        }

        private void CreateEntityPool()
        {
            pooledEntities = new List<T>();
            for (int i = 0; i < poolCount; i++)
            {
                pooledEntities.Add(NewEntity());
            }
        }

        private T NewEntity()
        {
            T entity = Instantiate(entityPrefab, transform);
            entity.gameObject.SetActive(false);
            return entity;
        }

        public T GetEntity()
        {
            if (pooledEntities == null || pooledEntities.Count == 0)
            {
                CreateEntityPool();
            }

            T entity = pooledEntities.Find(x => !x.gameObject.activeSelf);
            if (entity == null)
            {
                entity = NewEntity();
                pooledEntities.Add(entity);
            }

            return entity;
        }

        public void Reset()
        {
            foreach(var entity in pooledEntities)
            {
                entity.transform.SetParent(transform);
                entity.gameObject.SetActive(false);
                entity.transform.localPosition = Vector2.zero;
            }
        }
    } 
}
