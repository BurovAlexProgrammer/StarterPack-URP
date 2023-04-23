using System;
using System.Threading;
using UnityEngine;

namespace _Project.Scripts.Main.Wrappers
{
    public abstract class MonoPoolItemBase : MonoBehaviour
    {
        private static int _idGenerator;
        private static int NewId => _idGenerator++;
        
        private int _id = -1;
        private CancellationToken _destroyCancellationToken;

        public Action<MonoPoolItemBase> Returned;
        
        public int Id
        {
            get
            {
                _id = _id < 0 ? NewId : _id;
                return _id;
            }
        }
        
        public void ReturnToPool()
        {
            if (_destroyCancellationToken.IsCancellationRequested) return;
            
            gameObject.SetActive(false);
            Returned?.Invoke(this);
        }
    }
}