using System;

namespace WalkingPeople.Scripts.Core.Service
{
    public interface IService
    {
        Type ServiceType { get; }
    }
}