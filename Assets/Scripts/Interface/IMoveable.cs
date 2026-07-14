using SO;
using UnityEngine;

namespace Interface
{
    public interface IMoveable
    {
        MovementDataSo MovementData { get; }
        Rigidbody2D RbCompo { get;}
        
        void Move(Vector2 direction);
        void SetMoveDir(Vector2 moveDir);
    }
}