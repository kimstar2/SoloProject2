namespace Interface
{
    public interface IDash : IAgentModule
    {
        float DashMulti { get; }
        float DashDur { get; }
        float DashCool { get; }
        bool IsDash { get; }
        
        void Dash();
    }
}