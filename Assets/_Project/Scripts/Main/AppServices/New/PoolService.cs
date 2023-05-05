using System.Collections.Generic;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    public class PoolService : IService, IConstruct
    {
        private Dictionary<GameObject, MonoPool> _poolDictionary;
        private Transform _itemsContainer;

        public void Init()
        {
            _poolDictionary = new Dictionary<GameObject, MonoPool>();
        }

        public GameObject GetAndActivate(GameObject prefab)
        {
            var result = Get(prefab);
            result.gameObject.SetActive(true);
            return result;
        }

        public MonoPool CreatePool(GameObject prefab, int initialCapacity = 1, int maxCapacity = 20, MonoPool.OverAllocationBehaviour behaviour = MonoPool.OverAllocationBehaviour.Warning)
        {
            _poolDictionary ??= new Dictionary<GameObject, MonoPool>();
            
            if (_poolDictionary.ContainsKey(prefab))
            {
                return _poolDictionary[prefab];
            }

            return new MonoPool(prefab, _itemsContainer, initialCapacity, maxCapacity, behaviour);
        }
        
        public GameObject Get(GameObject prefab)
        {
            if (_poolDictionary == null)
            {
                Debug.LogWarning("Pool created automatically by call method PoolService.Get(Prefab).");
                CreatePool(prefab);
            }
            
            if (!_poolDictionary.ContainsKey(prefab))
            {
                var newContainer = new GameObject(prefab.name);
                newContainer.transform.SetParent(_itemsContainer);
                var newPool = new MonoPool(prefab, newContainer.transform, 10, 20);
                _poolDictionary.Add(prefab, newPool);
            }

            return _poolDictionary[prefab].Get();
        }

        public void Reset()
        {
            foreach (var (key, pool) in _poolDictionary)
            {
                pool.DeactivateItems();
            }
        }

        public void Construct()
        {
            var poolService = new GameObject() { name = "Pool Service"};
            poolService.transform.SetParent(AppContext.ServicesHierarchy);
            _itemsContainer = poolService.transform;
        }
    }
}