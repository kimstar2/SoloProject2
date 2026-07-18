using System;
using Interface;
using UnityEngine;

namespace SO.Event
{
    [CreateAssetMenu(menuName = "SO/Events/Card Apply Event Channel")]
    public class CardApplyEventChannel : ScriptableObject
    {
        public event Action<GameObject, ICard> OnEventRaised;

        public void RaiseEvent(GameObject target, ICard card)
        {
            OnEventRaised?.Invoke(target, card);
        }
    }
}