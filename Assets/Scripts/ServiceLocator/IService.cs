using System;

namespace ServiceLocator
{
    public interface IService
    {
        Type ServiceType { get; }
    }
}