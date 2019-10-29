namespace WalkingPeople.Scripts.Core.MVC.ObserverLogic
{
    public interface IObservable
    {
        void OnObjectChanged(IObserver observer);
    }
}
