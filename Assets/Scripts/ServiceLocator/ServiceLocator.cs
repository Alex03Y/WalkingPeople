using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ServiceLocator
{
    public class ServiceLocator : MonoBehaviour
    {
        private static readonly Dictionary<Type, object> ServiceMap = new Dictionary<Type, object>();
//        public static List<ResolveObject> ResolveObjects = new List<ResolveObject>();

        public static void Register(Type serviceType, object instance)
        {
            if (ServiceMap.ContainsKey(serviceType))
            {
                throw new Exception($"[ServiceLocator] Service {serviceType.Name} already registered.");
            }
        
            ServiceMap.Add(serviceType, instance);

//            var resolveList = ResolveObjects.Where(x => x.ResolveType == serviceType);
//            foreach (var resolveObject in resolveList)
//                resolveObject.Invoke(instance);
        }

        public static TRegister Resolve<TRegister>() where TRegister : class
        {
            TRegister instance = null;
            var serviceType = typeof(TRegister);

            if (ServiceMap.ContainsKey(serviceType))
            {
                instance = (TRegister) ServiceMap[serviceType];
            }
            else
            {
                Debug.LogError($"[ServiceLocator] Service {serviceType.Name} was not being register.");
            }
        
            return instance;
        }
        
//        public static void Resolve<TRegister>(Action<TRegister> onResolved) where TRegister : class
//        {
//            TRegister instance = null;
//            var serviceType = typeof(TRegister);

//            if (ServiceMap.ContainsKey(serviceType))
//            {
//                instance = (TRegister) ServiceMap[serviceType];
//                onResolved?.Invoke(instance);
//            }
//            else
//            {
//                foreach (var resolveObject in ResolveObjects)
//                {
//                    if(resolveObject.ResolveType != serviceType)
//                        Debug.LogError($"Cyclic dependency between {resolveObject.ResolveType.FullName} and {serviceType.FullName}");
//                }
                
//                ResolveObjects.Add(new ResolveObjectEvent<TRegister>
//                {
//                    ResolveType = serviceType,
//                    OnResolved = onResolved
//                });
//            }
//        }
    }

//    public abstract class ResolveObject
//    {
//        public Type ResolveType;
//        public abstract void Invoke(object o);
//    }

//    public class ResolveObjectEvent<T> : ResolveObject
//    {
//        public Action<T> OnResolved;
//        public override void Invoke(object o) => OnResolved?.Invoke((T) o);
//    }
}