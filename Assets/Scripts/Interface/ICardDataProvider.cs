using SO;

namespace Interface
{
    public interface ICardDataProvider : IDataProvider
    {
        public CardDataSo CardData { get; }
    }
}