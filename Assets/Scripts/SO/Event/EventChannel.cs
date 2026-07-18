using System;
using UnityEngine;

namespace SO.Event
{
    [CreateAssetMenu(menuName = "SO/Events/Void Event Channel", order = 0)]
    public class EventChannel : ScriptableObject
    {
        public event Action OnEvent;

        public void RaiseEvent()
        {
            OnEvent?.Invoke();
        }
    }
}