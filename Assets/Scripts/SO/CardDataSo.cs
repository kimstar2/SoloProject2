using Enum;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Card data", menuName = "SO/Card/Card data", order = 0)]
    public class CardDataSo : ScriptableObject
    {
        [Header("Stats")]
        public StatType statType;
        public float statAddValue;
        public float statMultiValue;
    }
}
