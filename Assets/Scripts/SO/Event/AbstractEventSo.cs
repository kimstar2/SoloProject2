using System;
using UnityEngine;

namespace SO.Event
{
    public abstract class AbstractEventSo<T> : ScriptableObject
    {
        public event Action<T> OnEvent;

        public void RaiseEvent(T arg)
        {
            OnEvent?.Invoke(arg);
        }
    } 
}