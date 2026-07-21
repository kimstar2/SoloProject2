using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ForRigid2D : MonoBehaviour
    {
        protected Rigidbody2D RbCompo;

        protected virtual void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
        }
    }
}