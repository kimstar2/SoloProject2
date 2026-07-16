using UI.Card;

namespace Interface
{
    public interface ICardDropTarget
    {
        bool CanReceive(ICard card);
        void ReceiveCard(ICard card);
    }
}