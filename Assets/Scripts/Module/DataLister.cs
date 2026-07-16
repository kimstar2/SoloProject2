    using System.Collections.Generic;
using Enum;
using Interface;
using RunTimeData;
using SO;
using UnityEngine;

namespace Module
{
    public class DataLister : MonoBehaviour, IModule, IDataLister
    {
        public readonly List<IStatDataProvider> OriginDataList = new();

        private readonly Dictionary<StatType, RunTimeStat> _runtimeStatMap = new();

        private void Awake()
        {
            OriginDataList.AddRange(GetComponentsInChildren<IStatDataProvider>());

            foreach (IStatDataProvider provider in OriginDataList)
            {
                StatDataSo originData = provider.StatData;
                if (originData == null)
                    continue;
                if (_runtimeStatMap.ContainsKey(originData.statType))
                {
                    Debug.LogError($"Duplicate stat type {originData.statType} in {gameObject.name}",this);
                    continue;
                }
                RunTimeStat runTimeStat = new(originData);
                _runtimeStatMap.Add(originData.statType, runTimeStat);
            }
        }

        public bool TryGetRuntimeStat(StatType statType, out RunTimeStat runtimeStat)
        {
            return _runtimeStatMap.TryGetValue(statType, out runtimeStat);
        }
    }
}