using Enum;
using Interface;
using SO;
using UnityEngine;

namespace DataProvider
{
    public class StatDataProvider : MonoBehaviour , IStatDataProvider
    {
        [field:SerializeField] public StatDataSo StatData { get; private set; }

        private void OnValidate()
        {
            if (StatData == null) return;
            switch (StatData.statType)
            {
                case StatType.Health:
                    gameObject.name = "Health Stat";
                    break;
                case StatType.Defense:
                    gameObject.name = "Defense Stat";
                    break;
                case StatType.Damage:
                    gameObject.name = "Damage Stat";
                    break;
                case StatType.Mana:
                    gameObject.name = "Mana Stat";
                    break;
            }
            gameObject.name += $"(default:{StatData.statValue})";
        }
    }
}