using System.Collections.Generic;
using Struct;
using UnityEngine;
using UnityEngine.Events;

namespace SO
{
    [CreateAssetMenu(fileName = "TrmActOption", menuName = "SO/UI/TrmActOption", order = 0)]
    public class TransformActionOptionSo : ScriptableObject
    {
        public List<TransformAction> onActions = new();
        public List<TransformAction> offActions = new();
    }
}