using Interface;
using Module.Caster;
using SO.HitEffect;
using Struct;
using UnityEngine;

namespace RunTimeData
{
    public class GrowingProjectileHitEffect : IHitEffect, IGetOwner<Caster>
    {
        private readonly GrowingProjectileHitEffectSo _data;
        private int _currentStack;
        private Vector3 _originScale;
        private Caster _ownerCaster;

        public GrowingProjectileHitEffect(GrowingProjectileHitEffectSo data)
        {
            _data = data;
        }

        public float ModifyDamage(float damage) => damage; // nothing

        public void AfterHit(in HitContext context)
        {
            _currentStack = Mathf.Min(_currentStack + 1, _data.MaxStack);
            float multiplier = 1f + _currentStack * _data.IncreaseRate;

            _ownerCaster.TargetTransform.localScale = _originScale * multiplier;
            _ownerCaster.SetScaleMultiplier(multiplier);
        }

        public void Get(Caster owner)
        {
            _ownerCaster = owner;
            _originScale = _ownerCaster.TargetTransform.localScale;
        }
    }
}