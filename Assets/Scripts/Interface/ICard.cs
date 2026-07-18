using Vector2 = UnityEngine.Vector2;

namespace Interface
{
    public interface ICard
    {
        bool IsInDeck { get; }
        void SetLayout(Vector2 pos , float zRot);
        void KillSeq();
        ICardDataProvider[] GetData();
    }
}