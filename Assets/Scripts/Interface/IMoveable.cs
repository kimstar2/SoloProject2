using UnityEngine;

namespace Interface
{
    public interface IMoveable
    {
        Rigidbody2D RbCompo { get;}
        float Speed { get;}
        
        void Move(Vector2 direction);
        void SetMoveDir(Vector2 moveDir);
    }
}