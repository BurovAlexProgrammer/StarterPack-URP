using System;
using System.Collections.Generic;
using _Project.Scripts.Main.Events;

namespace _Project.Scripts.Main.Systems
{
    public interface ISystem
    {
        Dictionary<Type, Action<BaseEvent>> EventCallbacks { get; }
        void Init();
        void AddListener<T>(Action<BaseEvent> callback) where T : BaseEvent;
        void RemoveListener<T>();
        void AddEventHandlers();
        void RemoveEventHandlers();
    }
}