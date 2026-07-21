using Interface;
using SO;
using UnityEngine;
using UnityEngine.Serialization;

namespace DataProvider
{
    public class StatDataProvider : MonoBehaviour , IStatDataProvider
    {
        [FormerlySerializedAs("<StatData>k__BackingField")]
        [SerializeField] private StatDataSo statData;

        public StatDataSo StatData => statData;

        private void OnValidate()
        {
            if (StatData == null) return;
            gameObject.name = $"{StatData.statType} Stat (Default: {StatData.statValue})";
        }
    }
}
