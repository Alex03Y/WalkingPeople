namespace WalkingPeople.Scripts.MVC.Observer
{
    public interface IObservable
    {
        void OnObjectChanged(IObserver observer);

    }
}
