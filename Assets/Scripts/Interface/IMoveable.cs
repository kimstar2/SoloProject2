using UnityEngine;

namespace Interface
{
    public interface IMoveable
    {
        float Speed { get;}
        void Move(Vector2 direction);
    }
}