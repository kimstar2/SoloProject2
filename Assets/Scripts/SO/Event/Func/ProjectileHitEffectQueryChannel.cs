using System.Collections.Generic;
using SO.HitEffect;
using UnityEngine;

namespace SO.Event.Func
{
    [CreateAssetMenu(menuName = "SO/Events/Func/Hit Effect Query", order = 0)]
    public class ProjectileHitEffectQueryChannel : AbstractFunc<IEnumerable<HitEffectSo>> {}
}