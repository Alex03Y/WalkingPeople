using System;
using UnityEngine;

namespace WalkingPeople.Scripts.Pool
{
    [Serializable]
    public class PoolObject
    {
        public Transform Transform { get; }
        public GameObject GameObject { get; }
        public IPoolObject[] PoolObjectScripts { get; }
        public Transform Parent { get; }

        private PoolManager _poolManager;

        public PoolObject(GameObject instance, PoolManager poolManager, Transform parent)
        {
            GameObject = instance;
            Transform = instance.transform;
            PoolObjectScripts = instance.GetComponentsInChildren<IPoolObject>(true);
            
            _poolManager = poolManager;
            Parent = parent;
        }
    
        public void Destroy()
        {
            _poolManager.DisposePoolObject(this);
        }
    }
}