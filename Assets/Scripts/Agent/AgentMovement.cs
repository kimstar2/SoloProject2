using Module;
using UnityEngine;

namespace Agent
{
    public class AgentMovement : MonoBehaviour , IModule
    {
        public ModuleCompo Owner { get; private set; }
        public void Init(ModuleCompo owner) => Owner = owner;
        
        [field:SerializeField] public Rigidbody2D RbCompo {get; private set;}
        [field:SerializeField] public float Speed {get; private set;}

        public void Move(Vector2 direction)
        {
            RbCompo.linearVelocity = direction * Speed;
        }
    }
}