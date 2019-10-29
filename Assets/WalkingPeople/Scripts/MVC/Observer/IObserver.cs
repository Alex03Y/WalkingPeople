namespace WalkingPeople.Scripts.MVC.Observer
{
    public interface IObserver
    {
        void AddObserver(IObservable observable);
        void RemoveObservable(IObservable observable);
        void SetChanged();


    }
}
