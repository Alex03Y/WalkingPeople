namespace WalkingPeople.Scripts.Core.MVC.ObserverLogic
{
    public interface IObserver
    {
        void AddObserver(IObservable observable);
        void RemoveObservable(IObservable observable);
        void SetChanged();
    }
}
