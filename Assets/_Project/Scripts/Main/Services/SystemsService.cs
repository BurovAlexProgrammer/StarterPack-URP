using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Main.Events;
using _Project.Scripts.Main.Systems;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.Main.Services
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

        public static void FireEvent<T>(T firedEvent) where T : BaseEvent
        {
            #if UNITY_EDITOR
            var color = EditorGUIUtility.isProSkin ? "#00952A" : "#017020";
            #else
            var color = "default";
            #endif
            
            Debug.Log($"Fired event <color={color}>{firedEvent.GetType().Name}</color>. {DateTime.Now.ToString("hh:mm:ss")}");
            
            
            foreach (var (key, system) in _systems)
            {
                if (system.EventCallbacks.ContainsKey(firedEvent.GetType()) == false) continue;
                
                system.EventCallbacks[firedEvent.GetType()]?.Invoke(firedEvent);
            }
        }

        public static void Dispose()
        {
            foreach (var key in _systems.Keys.ToList())
            {
                _systems[key] = null;
            }
            
            _systems.Clear();
        }
    }
}