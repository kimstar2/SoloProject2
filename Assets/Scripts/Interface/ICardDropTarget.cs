using Interface.Marker;

namespace Interface
{
    public interface ICardDropTarget : IModule
    {
        bool CanReceive(ICard card);
        bool ReceiveCard(ICard card);
    }
}