using System.Collections.Generic;
using Enum;
using Interface;
using Interface.Marker;
using RunTimeData;
using SO;
using UnityEngine;

namespace Module
{
    public class StatDataLister : MonoBehaviour, IModule
    {
        private readonly List<IStatDataProvider> _originDataList  = new();
        public IReadOnlyList<IStatDataProvider> DataList => _originDataList; 

        private readonly Dictionary<StatType, RunTimeStat> _runtimeStatMap = new();

        private void Awake()
        {
            _originDataList.AddRange(GetComponentsInChildren<IStatDataProvider>());

            foreach (IStatDataProvider provider in DataList)
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