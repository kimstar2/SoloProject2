using System;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Param Hash data", menuName = "SO/Anim/Param Hash data", order = 0)]
    public class ParamHashSo : ScriptableObject
    {
        public string parameter;
        public int paramHash;

        private void OnValidate()
        {
            paramHash = Animator.StringToHash(parameter);
        }
    }
}