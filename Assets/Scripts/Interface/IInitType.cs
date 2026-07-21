using UnityEngine;

namespace Interface
{
    public interface IInitType<in T> where T : Component 
    {
        public void Init(T target);
    }
}