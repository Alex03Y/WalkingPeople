using UnityEngine;

namespace WalkingPeople.Scripts.Core.Service
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