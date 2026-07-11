using System.Collections.Generic;
using Structure;
using UnityEngine;
using UnityEngine.Events;

namespace SO
{
    [CreateAssetMenu(fileName = "TrmActOption", menuName = "SO/UI/TrmActOption", order = 0)]
    public class TransformActionOptionSo : ScriptableObject
    {
        public List<TransformAction> onActions = new();
        public UnityEvent onEvent;
        public List<TransformAction> offActions = new();
        public UnityEvent offEvent;
    }
}