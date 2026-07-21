using Interface.Marker;

namespace Interface
{
    public interface IPressedAttackable : IModule
    {
        void Attack(bool isPressed);
    }
}