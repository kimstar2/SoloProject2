using Interface;
using UnityEngine;

namespace SO.HitEffect
{
    public abstract class HitEffectSo : ScriptableObject
    {
        public abstract IHitEffect CreateRunTimeEffect();
    }
}