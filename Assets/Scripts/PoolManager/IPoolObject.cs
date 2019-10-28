namespace PoolManager
{
    public interface IPoolObject
    {
        void OnAwake(PoolObject poolObject);
        void OnReuseObject();
        void OnDisposeObject();
    }
}