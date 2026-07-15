namespace Interface.ForObserver
{
    public interface ISubject
    {
        void RegisterObserver(IObserver o);
        void UnregisterObserver(IObserver o);
        void NotifyObservers();
    }
}