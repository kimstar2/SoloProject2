namespace Interface
{
    public interface ICardDropTarget
    {
        bool CanReceive(ICard card);
        bool ReceiveCard(ICard card);
    }
}