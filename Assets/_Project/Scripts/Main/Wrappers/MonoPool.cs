using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Main.Wrappers
{
    [Serializable]
    public class MonoPool
    {
        private GameObject _prefab;
        private int _initCapacity;
        private int _maxCapacity;
        private int _instanceCount;
        private Transform _container;
        private OverAllocationBehaviour _overAllocationBehaviour;

        private Queue<GameObject> _inactivePool;
        private List<GameObject> _activePool;

        public enum OverAllocationBehaviour
        {
            Warning,
            ReplaceFirst,
            DestroyFirst,
            DestructFirst
        }

        public MonoPool(GameObject prefab, Transform container, int initialCapacity, int maxCapacity, OverAllocationBehaviour behaviour = OverAllocationBehaviour.Warning)
        {
            _prefab = prefab;
            _container = container;
            _initCapacity = initialCapacity;
            _maxCapacity = maxCapacity;
            _overAllocationBehaviour = behaviour;
            _inactivePool = new Queue<GameObject>(_initCapacity);
            _activePool = new List<GameObject>(_initCapacity);
            
            for (var i = 0; i < _initCapacity; i++)
            {
                AddInstance();
            }
        }
        
        public GameObject Get()
        {
            if (_inactivePool.Count == 0)
            {
                AddInstance();
            }
            
            var instance = _inactivePool.Dequeue();
            _activePool.Add(instance);

            return instance;
        }

        public void Clear()
        {
            _instanceCount = 0;

            if (_inactivePool != null)
            {
                foreach (var item in _inactivePool.ToArray())
                {
                    Object.DestroyImmediate(item.gameObject);
                }
            }

            if (_activePool != null)
            {
                foreach (var item in _activePool.ToArray())
                {
                    Object.DestroyImmediate(item);
                }
            }
            
            _inactivePool = new Queue<GameObject>();
            _activePool = new List<GameObject>();
        }

        public void DeactivateItems()
        {
            if (_activePool != null)
            {
                foreach (var item in _activePool.ToArray())
                {
                    if (item.gameObject.activeSelf == false) continue; 
                        
                    item.SetActive(false);
                }
            }
        }

        private void AddInstance()
        {
            if (_instanceCount == _maxCapacity)
            {
                switch (_overAllocationBehaviour)
                {
                    case OverAllocationBehaviour.Warning:
                        Debug.LogWarning($"Pool of '{_prefab.name}' is over allocated.");
                        break;
                    case OverAllocationBehaviour.ReplaceFirst:
                        break;
                    case OverAllocationBehaviour.DestroyFirst:
                        break;
                    case OverAllocationBehaviour.DestructFirst:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            var instance = Object.Instantiate(_prefab, _container);
            instance.gameObject.name = _prefab.name + " " + (_inactivePool.Count + _activePool.Count + 1);
            instance.gameObject.SetActive(false);
            _inactivePool.Enqueue(instance);
            _instanceCount++;
        }

        private void OnItemReturn(GameObject item)
        {
            var index = _activePool.FindIndex(x => x == item);
            
            if (index < 0) return;
            
            _activePool.RemoveAt(index);
            _inactivePool.Enqueue(item);
        }
    }
}