using Enum;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Stat data", menuName = "SO/Stat/Stat data", order = 0)]
    public class StatDataSo : ScriptableObject
    {
        public StatType statType;
        public float statValue;
    }
}