using System;
using System.Collections.Generic;
using UnityEngine;

namespace WalkingPeople.Scripts.Core.Pool
{
    public class PoolManager : MonoBehaviour
    {
        
        public static PoolManager instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private readonly Dictionary<int, Queue<PoolObject>> _poolMap = new Dictionary<int, Queue<PoolObject>>();
    
        public void CreatePool(GameObject prefab, int poolSize)
        {
            var poolKey = prefab.GetInstanceID();
        
            if (_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} already exist.");

            var pool = new Queue<PoolObject>();
            var poolParent = new GameObject($"Pool_{prefab.name}").transform;
            poolParent.parent = transform;
        
            for (int i = 0; i < poolSize; i++)
            {
                // instantiate and disable objectа
                var instance = Instantiate(prefab, poolParent);
                instance.SetActive(false);
            
                var poolObject = new PoolObject(instance, this, poolParent);
            
                // assign pool object to each IPoolObject
                foreach (var iPoolObject in poolObject.PoolObjectScripts)
                    iPoolObject.OnAwake(poolObject);

                pool.Enqueue(poolObject);
            }
        
            _poolMap.Add(poolKey, pool);
        }

        public void ClearPool(GameObject prefab, bool destroyObjects)
        {
            var poolKey = prefab.GetInstanceID();
        
            if (!_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} not found.");

            var objects = _poolMap[poolKey];
            _poolMap.Remove(poolKey);

            if (destroyObjects)
                while (objects.Count != 0)
                    Destroy(objects.Dequeue().GameObject);
        }

        public PoolObject InstantiateFromPool(GameObject prefab)
        {
            var poolKey = prefab.GetInstanceID();
        
            if (!_poolMap.ContainsKey(poolKey))
                throw new Exception($"[PoolManager] Pool {prefab.name} not found.");

            var poolObject = _poolMap[poolKey].Dequeue();
            _poolMap[poolKey].Enqueue(poolObject);

            if (poolObject.GameObject.activeSelf)
                DisposePoolObject(poolObject);

            poolObject.GameObject.SetActive(true);
            foreach (var iPoolObject in poolObject.PoolObjectScripts)
                iPoolObject.OnReuseObject();

            return poolObject;
        }

        public PoolObject InstantiateFromPool(GameObject prefab, Vector3 position)
        {
            var poolObject = InstantiateFromPool(prefab);
            poolObject.Transform.position = position;
            return poolObject;
        }
    
        public PoolObject InstantiateFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var poolObject = InstantiateFromPool(prefab, position);
            poolObject.Transform.rotation = rotation;
            return poolObject;
        }

        public void DisposePoolObject(PoolObject poolObject)
        {
            poolObject.GameObject.SetActive(false);
            poolObject.Transform.position = Vector3.zero;
            poolObject.Transform.rotation = Quaternion.identity;
            poolObject.Transform.parent = poolObject.Parent;
        }
    }
}