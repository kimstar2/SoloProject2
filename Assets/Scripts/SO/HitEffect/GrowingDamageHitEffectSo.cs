using Interface;
using RunTimeData;
using UnityEngine;

namespace SO.HitEffect
{
    [CreateAssetMenu(fileName = "GrowingDamageHitEffect", menuName = "SO/HitEffect/Growing Damage", order = 0)]
    public class GrowingDamageHitEffectSo : HitEffectSo
    {
        [field: SerializeField] public float IncreaseRate { get; private set; } = 0.2f;
        [field: SerializeField] public int MaxStack { get; private set; } = 5;
        
        public override IHitEffect CreateRunTimeEffect()
        {
            return new GrowingDamageHitEffect(this);
        }
    }
}