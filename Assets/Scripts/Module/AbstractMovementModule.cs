using Interface;
using UnityEngine;

namespace Module
{
    public abstract class AbstractMovementModule : MonoBehaviour, IAgentModule
    {
        [field: SerializeField] public Rigidbody2D RbCompo { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        protected Vector2 MoveDir;

        public void SetMoveDir(Vector2 moveDir) => MoveDir = moveDir;
        public void Move(Vector2 direction) => RbCompo.linearVelocity = direction * Speed;
    }
}