using System;
using System.Collections.Generic;
using _Project.Scripts.Main.Events;

namespace _Project.Scripts.Main.Systems
{
    public abstract class BaseSystem : ISystem
    {
        public Dictionary<Type, Action<IEvent>> _eventCallbacks = new Dictionary<Type, Action<IEvent>>();

        public Dictionary<Type, Action<IEvent>> EventCallbacks => _eventCallbacks;

        public virtual void Init()
        {
        }

        public void AddListener<T>(Action<IEvent> callback) where T : IEvent
        {
            var type = typeof(T);

            if (callback == null) throw new Exception("Callback is null");
            
            if (_eventCallbacks.ContainsKey(type)) throw new Exception("Listener is registered already.");
            
            _eventCallbacks.Add(type, callback);
        }

        public void RemoveListener<T>()
        {
            var type = typeof(T);

            if (!_eventCallbacks.ContainsKey(type)) throw new Exception("Listener not found.");

            _eventCallbacks[type] = null;
            _eventCallbacks.Remove(type);
        }

        public virtual void AddEventHandlers()
        {
        }

        public virtual void RemoveEventHandlers()
        {
        }
    }
}