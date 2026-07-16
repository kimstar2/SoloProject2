using SO;

namespace Interface
{
    public interface IStatDataProvider : IDataProvider
    {
        public StatDataSo StatData { get; }
    }
}