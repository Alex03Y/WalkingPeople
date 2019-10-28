using UnityEngine;

namespace ServiceLocator
{
    public class ServiceRegister : MonoBehaviour
    {
        public bool IncludeInactive;
    
        private void Awake()
        {
            var services = GetComponentsInChildren<IService>(IncludeInactive);
            foreach (var service in services)
                ServiceLocator.Register(service.ServiceType, service);
        }
    }
}