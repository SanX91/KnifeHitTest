using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An abstract factory class which supports object pooling.
/// Can generate any type of objects which inherit from Unity's Component type.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PooledFactory<T> : MonoBehaviour where T : Component
{
    public T entityPrefab;
    public int poolCount = 10;
    private List<T> pooledEntities;

    private void Start()
    {
        CreateEntityPool();
    }

    /// <summary>
    /// Creates a pool of objects of a certain type and stores it in a list.
    /// </summary>
    private void CreateEntityPool()
    {
        pooledEntities = new List<T>();
        for (int i = 0; i < poolCount; i++)
        {
            pooledEntities.Add(NewEntity());
        }
    }

    /// <summary>
    /// Creates a new object of a certain type.
    /// </summary>
    /// <returns></returns>
    private T NewEntity()
    {
        T entity = Instantiate(entityPrefab, transform);
        entity.gameObject.SetActive(false);
        return entity;
    }

    /// <summary>
    /// Returns an unused and deactivated pooled object.
    /// Can create new objects, if run out of unused objects.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Resets the objects in the pool, to their original state.
    /// </summary>
    public void Reset()
    {
        foreach (T entity in pooledEntities)
        {
            entity.transform.SetParent(transform);
            entity.gameObject.SetActive(false);
            entity.transform.localPosition = Vector2.zero;
        }
    }
}

