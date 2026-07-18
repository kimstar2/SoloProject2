using System.Collections.Generic;
using Struct;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "SequenceStep", menuName = "SO/Tween/SequnceStep", order = 0)]
    public class SequenceStepSo : ScriptableObject
    {
        public List<SequenceTweenStep> steps = new();
    }
}