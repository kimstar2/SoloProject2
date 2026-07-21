using Interface;
using UnityEngine;

namespace Utility
{
    public class TypeInit<T>: MonoBehaviour , IInitType<T> where T : Component
    {
        protected T Owner { get; private set; }
        public virtual void Init(T target) => Owner = target; 
        
    }
}