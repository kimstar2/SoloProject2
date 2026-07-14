using SO;

namespace Interface
{
    public interface IDash : IAgentModule
    {
        DashDataSo DashData { get; }
        bool IsDash { get; }
        
        void Dash();
    }
}