using System;
using System.Collections.Generic;
using Interface;
using SO.CasterData;
using SO.HitEffect;
using Struct;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Module.Caster
{
    public class DamageCaster : MonoBehaviour , ICastable , IHitEffectReceiver
    {
        [field: SerializeField] public DamageCasterDataSo CasterDataSo { get; private set; }
        [field: SerializeField] public Caster Owner {get; private set;}
        [SerializeField] private List<HitEffectSo> defaultHitEffects = new();
        
        private readonly List<IHitEffect> _runTimeHitEffects = new();
        private readonly HashSet<IDamageable> _damagedTargets = new();

        private void OnEnable()
        {
            _damagedTargets.Clear();
            _runTimeHitEffects.Clear();

            foreach (var hitEffect in defaultHitEffects)
                AddHitEffect(hitEffect);
        }
        
        public void HandleSetData(CasterDataSo dataSo)
        {
            if (dataSo is not DamageCasterDataSo casterDataSo) return;
            CasterDataSo = casterDataSo;
        }
        
        public void Cast(Collider2D target)
        {
            if (CasterDataSo == null)
            {
                Debug.LogError("DamageCasterDataSo is not assigned.", this);
                return;
            }
            
            if (!target.TryGetComponent(out IDamageable damageable)) return;
            if (!_damagedTargets.Add(damageable)) return;

            float damage = CasterDataSo.usingRandomDamage
                ? Random.Range(CasterDataSo.minDamage, CasterDataSo.maxDamage)
                : CasterDataSo.defaultDamage;
            
            foreach(var hitEffect in _runTimeHitEffects)
                damage = hitEffect.ModifyDamage(damage);
            
            
            damageable.TakeDamage(damage);

            HitContext context =
                new(target, damageable, damage);
            
            foreach(var hitEffect in _runTimeHitEffects)
                hitEffect.AfterHit(context);
        }
        
        public void AddHitEffect(HitEffectSo effectSo)
        {
            if (effectSo == null) return;

            IHitEffect runtimeEffect = effectSo.CreateRunTimeEffect();

            if (runtimeEffect is IGetOwner<Caster> ownerEffect)
            {
                if (Owner == null)
                {
                    Debug.LogError($"{effectSo.name} requires a Caster owner.", this);
                    return;
                }

                ownerEffect.Get(Owner);
            }
            
            _runTimeHitEffects.Add(runtimeEffect);
        }
    }
}
