﻿using System.Collections.Generic;
using _Project.Scripts.Main.Game;
using _Project.Scripts.Main.Wrappers;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    public class PoolService : IService, IConstruct
    {
        private Dictionary<object, Pool> _poolDictionary = new Dictionary<object, Pool>();
        private Transform _itemsContainer;

        public void Init()
        {
            _poolDictionary = new Dictionary<object, Pool>();
        }

        public object GetAndActivate(object prefab)
        {
            var poolItem = Get(prefab);
            poolItem.GameObject?.SetActive(true);
            return poolItem;
        }

        public Pool CreatePool(object objectRef, int initialCapacity = 1, int maxCapacity = 20, Pool.OverAllocationBehaviour behaviour = Pool.OverAllocationBehaviour.Warning)
        {
            _poolDictionary ??= new Dictionary<object, Pool>();
            
            if (_poolDictionary.ContainsKey(objectRef))
            {
                return _poolDictionary[objectRef];
            }
            
            Transform poolContainer;

            if (objectRef is GameObject gameObject)
            {
                poolContainer = new GameObject(gameObject.name).transform;
                poolContainer.transform.SetParent(_itemsContainer);
            }
            else if (objectRef is MonoBehaviour monoBehaviour)
            {
                poolContainer = new GameObject(monoBehaviour.gameObject.name).transform;
                poolContainer.transform.SetParent(_itemsContainer);
            }
            else
            {
                poolContainer = _itemsContainer;
            }
            
            var newPool = new Pool(objectRef, poolContainer.transform, initialCapacity, maxCapacity, behaviour);
            _poolDictionary.Add(objectRef, newPool);
            
            return newPool;
        }
        
        public PoolItem Get(object objectKey)
        {
            if (_poolDictionary == null || !_poolDictionary.ContainsKey(objectKey))
            {
                Debug.LogWarning("Pool created automatically by call method PoolService.Get(Prefab).");
                CreatePool(objectKey);
            }

            return _poolDictionary[objectKey].Get();
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