using System.Collections.Generic;
using Enum;
using Interface;
using Module;
using RunTimeData;
using UI;
using UnityEngine;

namespace DataProvider
{
    public class StatUIProvider : MonoBehaviour
    {
        [SerializeField] private StatDataLister statDataLister;
        private readonly Dictionary<StatType , StatUI> _statUIDict = new();

        private void Start()
        {
            foreach (IStatDataProvider provider in statDataLister.DataList)
            {
                if (statDataLister.TryGetRuntimeStat(provider.StatData.statType, out RunTimeStat stat))
                {
                    StatUI myStat = MakeText();
                    _statUIDict.Add(provider.StatData.statType, myStat);
                    
                    myStat.SetType(provider.StatData.statType);
                    stat.Value.OnValueChanged += myStat.SetText;
                    myStat.SetText(0,stat.Value.Value);
                }
            }
        }

        private StatUI MakeText()
        {
            GameObject textObject = new GameObject();
            textObject.transform.SetParent(transform, false);
            return textObject.AddComponent<StatUI>();
        }

        private void OnDestroy()
        {
            foreach (IStatDataProvider provider in statDataLister.DataList)
            {
                if (statDataLister.TryGetRuntimeStat(provider.StatData.statType, out RunTimeStat stat))
                {
                    _statUIDict.TryGetValue(provider.StatData.statType, out StatUI myStat);
                    if (myStat != null)
                        stat.Value.OnValueChanged -= myStat.SetText;
                }
            }
            _statUIDict.Clear();
        }
    }
}