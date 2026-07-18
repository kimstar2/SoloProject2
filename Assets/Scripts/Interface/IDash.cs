using Interface.Marker;
using SO;

namespace Interface
{
    public interface IDash : IModule
    {
        DashDataSo OriginDashData { get; }
        bool IsDash { get; }
        
        bool TryDash();
    }
}