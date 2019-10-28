using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolManager
{
    [Serializable]
    public class PoolObject
    {
        public Transform Transform { get; }
        public GameObject GameObject { get; }
        public IPoolObject[] PoolObjectScripts { get; }
        public Transform Parent { get; }

        private readonly Dictionary<Type, List<Component>> _cachedComponentsMap = new Dictionary<Type, List<Component>>();
        private PoolManager _poolManager;

        public PoolObject(GameObject instance, PoolManager poolManager, Transform parent)
        {
            GameObject = instance;
            Transform = instance.transform;
            PoolObjectScripts = instance.GetComponentsInChildren<IPoolObject>(true);
            
            _poolManager = poolManager;
            Parent = parent;
        }
    
//        public void CacheComponent(Component component)
//        {
//            var componentType = component.GetType();
//            if (_cachedComponentsMap.ContainsKey(componentType))
//            {
//                var componentsList = _cachedComponentsMap[componentType];
//            
//                if (componentsList.Exists(x => x.GetInstanceID() == component.GetInstanceID()))
//                {
//                    Debug.LogError("[PoolManager] Component with same id already cached.");
//                    return;
//                }
//            
//                componentsList.Add(component);
//            }
//            else _cachedComponentsMap.Add(componentType, new List<Component> {component});
//        }

//        public T GetCachedComponent<T>() where T : Component
//        {
//            var componentType = typeof(T);
//        
//            if (_cachedComponentsMap.ContainsKey(componentType)) 
//                return (T) _cachedComponentsMap[componentType][0];
//        
//            Debug.LogError("[PoolManager] Component not being cached.");
//            return null;
//        }
//
//        public bool GetComponents<T>(out List<Component> components) where T : Component
//        {
//            var componentType = typeof(T);
//            if (!_cachedComponentsMap.ContainsKey(componentType))
//            {
//                components = null;
//                return false;
//            }
//
//            components = _cachedComponentsMap[componentType];
//            return true;
//        }

        public void Destroy()
        {
            _poolManager.DisposePoolObject(this);
        }
    }
}