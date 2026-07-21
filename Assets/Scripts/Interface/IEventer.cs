using Interface.Marker;

namespace Interface
{
    public interface IEventer : IModule
    {
        void EnableEvent();
        void DisableEvent();
    }
}