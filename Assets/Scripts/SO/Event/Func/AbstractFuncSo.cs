using System;
using UnityEngine;

namespace SO.Event.Func
{
    public abstract class AbstractFunc<T> : ScriptableObject
    {
        public event Func<T> OnEvent;

        public T RaiseEvent()
        {
            if (OnEvent == null) return default;

            Delegate[] providers = OnEvent.GetInvocationList();
            if (providers.Length > 1)
            {
                Debug.LogError(
                    $"{name} has {providers.Length} query providers. A query channel must have exactly one provider.",
                    this);
            }

            return ((Func<T>)providers[0]).Invoke();
        }
    } 
}
