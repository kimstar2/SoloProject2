using Interface;
using UnityEngine;
using Utility;

namespace Object.ProjectileObject
{
    public abstract class Projectile : ForRigid2D, IProjectile
    {
        [field:SerializeField] public float Speed { get; private set; }
        public void InitAndFire(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);

            Vector2 direction = rotation * Vector2.right;

            RbCompo.linearVelocity = direction * Speed;
        }
    }
}
