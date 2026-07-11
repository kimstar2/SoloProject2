using System;
using System.Collections.Generic;
using System.Linq;
using Interface;
using UnityEngine;

namespace Module
{
    public class AgentModule : MonoBehaviour
    {
        private Dictionary<Type, IAgentModule> _moduleDict;

        protected virtual void Awake()
        {
            _moduleDict = GetComponentsInChildren<IAgentModule>()
                .GroupBy(module => module
                    .GetType())
                .ToDictionary(g => g.Key, g => g.First());
        }

        protected T GetModule<T>() where T : class, IAgentModule
        {
            return _moduleDict.Values.OfType<T>().FirstOrDefault();
        }
    }
}