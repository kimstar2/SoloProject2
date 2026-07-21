using UnityEngine;

namespace Interface
{
    public interface IProjectile
    {
        float Speed {get; }
        void InitAndFire(Vector3 position, Quaternion rotation);
    }
}