using System;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public class Subscriber : MonoBehaviour, IModule
    {
        private readonly List<Action> _unSubActions = new List<Action>();
        public ModuleCompo Owner { get; private set; }
        public void Init(ModuleCompo owner) => Owner = owner;

        public void Subscribe(Action subAction, Action unSubAction)
        {
            subAction?.Invoke();
            if (unSubAction != null)
                _unSubActions.Add(unSubAction);
        }

        public void Unsubscribe()
        {
            Action[] actionsToInvoke = _unSubActions.ToArray();
            _unSubActions.Clear();
            foreach (Action unSubAction in actionsToInvoke)
                unSubAction?.Invoke();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }
    }
}