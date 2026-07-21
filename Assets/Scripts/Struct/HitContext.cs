using Interface;
using UnityEngine;

namespace Struct
{
    public struct HitContext
    {
        public Collider2D Target { get; }
        public IDamageable Damageable { get; }
        public float Damage { get; }

        public HitContext(Collider2D target, IDamageable damageable, float damage)
        {
            Target = target;
            Damageable = damageable;
            Damage = damage;
        }
    }
}