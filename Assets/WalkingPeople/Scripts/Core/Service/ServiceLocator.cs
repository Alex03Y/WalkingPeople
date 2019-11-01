using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WalkingPeople.Scripts.Core.Service
{
    public class ServiceLocator : MonoBehaviour
    {
        private static readonly Dictionary<Type, object> ServiceMap = new Dictionary<Type, object>();

        private void Awake()
        {
            AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x
                    .GetTypes()
                    .Where(t => 
                        !t.IsAbstract && 
                        t.GetInterfaces().Contains(typeof(IService)) &&
                        !t.IsSubclassOf(typeof(MonoBehaviour))))
                .ToList()
                .ForEach(x =>
                {
                    var instance = (IService) Activator.CreateInstance(x);
                    Register(instance.ServiceType, instance);
                });
        }

        public static void Register(Type serviceType, object instance)
        {
            if (ServiceMap.ContainsKey(serviceType))
            {
                throw new Exception($"[ServiceLocator] Service {serviceType.Name} already registered.");
            }
        
            ServiceMap.Add(serviceType, instance);
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

        private void OnDestroy()
        {
            ServiceMap.Clear();
        }
    }
}