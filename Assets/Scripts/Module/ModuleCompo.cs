using System;
using System.Collections.Generic;
using System.Linq;
using Interface;
using Interface.Marker;
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
        }

        protected T GetModule<T>() where T : class, IModule
        {
            return _moduleDict.Values.OfType<T>().FirstOrDefault();
        }
    }
}