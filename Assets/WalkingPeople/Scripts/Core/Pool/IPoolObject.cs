namespace WalkingPeople.Scripts.Core.Pool
{
    public interface IPoolObject
    {
        void OnAwake(PoolObject poolObject);
        void OnReuseObject();
        void OnDisposeObject();
    }
}