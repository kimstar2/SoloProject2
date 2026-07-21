using Interface;
using SO.HitEffect;
using Struct;
using UnityEngine;

namespace RunTimeData
{
    public class GrowingDamageHitEffect : IHitEffect
    {
        private readonly GrowingDamageHitEffectSo _data;
        private int _currentStack;

        public GrowingDamageHitEffect(GrowingDamageHitEffectSo data)
        {
            _data = data;
        }

        public float ModifyDamage(float damage)
        {
            float multiplier =
                1f + _currentStack * _data.IncreaseRate;

            return damage * multiplier;
        }

        public void AfterHit(in HitContext context)
        {
            _currentStack = Mathf.Min(_currentStack + 1, _data.MaxStack);
        }
    }
}