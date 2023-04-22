using System;
using System.Collections.Generic;
using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Systems;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    public static class SystemsService
    {
        private static Dictionary<Type, ISystem> _systems = new Dictionary<Type, ISystem>();

        public static T Bind<T>() where T : ISystem
        {
            var type = typeof(T);

            if (_systems.ContainsKey(type)) throw new Exception("System has bound already.");

            var newSystem = Activator.CreateInstance<T>(); 
            _systems.Add(type, newSystem);
            newSystem.Init();
            newSystem.AddEventHandlers();
            return newSystem;
        }

        public static void FireEvent<T>(T firedEvent) where T : IEvent
        {
            Debug.Log("Fired");
            
            foreach (var (key, system) in _systems)
            {
                if (system.EventCallbacks.ContainsKey(firedEvent.GetType()) == false) return;
                
                system.EventCallbacks[firedEvent.GetType()]?.Invoke(firedEvent);
            }
        } 
    }
}