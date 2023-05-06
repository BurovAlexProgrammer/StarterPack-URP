using System;
using System.Threading;
using _Project.Scripts.Extension;
using UnityEngine;
using Object = System.Object;

namespace _Project.Scripts.Main.Wrappers
{
    public sealed class PoolItem
    {
        private static UInt64 _idGenerator;
        private static UInt64 NewId => _idGenerator++;

        public readonly object Object;
        public readonly Type Type;
        public Action<PoolItem> Returned;
        
        private UInt64 _id = 0;
        private int _index = -1;
        private GameObject _gameObject;
        private MonoBehaviour _component;
        private CancellationToken _destroyCancellationToken;
        
        public UInt64 Id => _id;
        public GameObject GameObject
        {
            get
            {
                if (_gameObject == null)
                {
                    Debug.LogError($"Pool of objectKey: \"{nameof(Object)}\" is not GameObject type.");
                    return default;
                } 
                return Object as GameObject;
            }
        }

        public MonoBehaviour Component
        {
            get
            {
                if (_component == null)
                {
                    Debug.LogError($"Pool of objectKey: \"{nameof(Object)}\" is not MonoBehaviour type.");
                    return default;
                } 
                return Object as MonoBehaviour;
            }
        }

        public int Index => _index;

        public PoolItem(object obj, int index)
        {
            Object = obj;
            Type = obj.GetType();
            if (obj is GameObject)
            {
                _gameObject = obj as GameObject;
                _gameObject.name = _gameObject.CleanName() + " " + index;
            }

            if (obj is MonoBehaviour monoBehaviour)
            {
                _gameObject = monoBehaviour.gameObject;
            }
            
            _id = NewId;
            _index = index;
        }

        public void SetName(string nameTemplate)
        {
            if (_gameObject == null) return;

            _gameObject.name = nameTemplate;
        }
        
        public void ReturnToPool()
        {
            if (_destroyCancellationToken.IsCancellationRequested) return;
            
            GameObject.SetActive(false);
            Returned?.Invoke(this);
        }
    }
}