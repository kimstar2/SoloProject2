using SO;

namespace Interface
{
    public interface IDash : IModule
    {
        DashDataSo OriginDashData { get; }
        bool IsDash { get; }
        
        void Dash();
    }
}