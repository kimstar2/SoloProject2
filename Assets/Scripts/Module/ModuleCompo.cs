using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Module
{
    public class ModuleCompo : MonoBehaviour
    {
        private Dictionary<Type, IModule> _moduleDict;

        protected virtual void Awake()
        {
            _moduleDict = GetComponentsInChildren<IModule>()
                .GroupBy(module => module
                    .GetType())
                .ToDictionary(g => g.Key, g => g.First());
            foreach (IModule module in _moduleDict.Values)
                module.Init(this);
        }

        public T GetModule<T>() where T : class , IModule
        {
            if (_moduleDict.TryGetValue(typeof(T), out var module))
                return module as T;
            return null;
        }
    }
}