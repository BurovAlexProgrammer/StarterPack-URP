using System;
using System.Collections.Generic;
using _Project.Scripts.Main.Events;

namespace _Project.Scripts.Main.Systems
{
    public interface ISystem
    {
        Dictionary<Type, Action<IEvent>> EventCallbacks { get; }
        void Init();
        void AddListener<T>(Action<IEvent> callback) where T : IEvent;
        void RemoveListener<T>();
        void AddEventHandlers();
        void RemoveEventHandlers();
    }
}