using Struct;

namespace Interface
{
    public interface IHitEffect
    {
        float ModifyDamage(float damage);
        void AfterHit(in HitContext context);
    }
}