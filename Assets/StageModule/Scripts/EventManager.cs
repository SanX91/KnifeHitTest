using System;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class EventManager
    {
        private static readonly Lazy<EventManager> instance
          = new Lazy<EventManager>(() => new EventManager());

        private EventManager() { }

        public static EventManager Instance => instance.Value;

        private Dictionary<Type, Action<IEvent>> listeners = new Dictionary<Type, Action<IEvent>>();
        private Dictionary<Delegate, Action<IEvent>> lookup = new Dictionary<Delegate, Action<IEvent>>();

        public void AddListener<T>(Action<T> action) where T : IEvent
        {
            if (action == null)
            {
                Debug.Log("Action cannot be null");
                return;
            }

            if (lookup.ContainsKey(action))
            {
                Debug.Log("Action already added");
                return;
            }

            Action<IEvent> callback = (e) => action((T)e);
            lookup.Add(action, callback);

            Type type = typeof(T);
            if (listeners.TryGetValue(type, out Action<IEvent> existingAction))
            {
                listeners[type] = existingAction += callback;
                Debug.Log($"Action successfully added: {lookup.Count}");
                return;
            }

            listeners[type] = callback;
            Debug.Log($"Action successfully added: {lookup.Count}");
        }

        public void RemoveListener<T>(Action<T> action) where T : IEvent
        {
            if (action == null)
            {
                Debug.Log("Action cannot be null");
                return;
            }

            if (lookup.TryGetValue(action, out Action<IEvent> callback))
            {
                Type type = typeof(T);
                if (listeners.TryGetValue(type, out Action<IEvent> existingAction))
                {
                    existingAction -= callback;
                    if (existingAction == null)
                    {
                        listeners.Remove(type);
                    }
                    else
                    {
                        listeners[type] = existingAction;
                    }
                }

                lookup.Remove(action);
                Debug.Log($"Action successfully removed: {lookup.Count}");
                return;
            }

            Debug.Log($"Action not found");
        }

        public void TriggerEvent(IEvent evt)
        {
            Type type = evt.GetType();
            if (listeners.TryGetValue(type, out Action<IEvent> action))
            {
                action.Invoke(evt);
                return;
            }

            Debug.LogWarning($"Event has no event listeners {type}");
        }
    }
}
