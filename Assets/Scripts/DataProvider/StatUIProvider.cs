using Interface;
using Module;
using RunTimeData;
using TMPro;
using UnityEngine;

namespace DataProvider
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class StatUIProvider : MonoBehaviour , IInitType
    {
        private TextMeshProUGUI _statText;
        private DataLister _dataLister;

        private void Awake()
        {
            _statText = GetComponent<TextMeshProUGUI>();
        }
        public void Init<T>(T type)
        {
            _dataLister = type as DataLister;
        }

        private void Start()
        {
            foreach (var d in _dataLister.OriginDataList)
            {
                if (_dataLister.TryGetRuntimeStat(d.StatData.statType, out RunTimeStat stat))
                    stat.Value.OnValueChanged += SetText;
            }
        }

        private void SetText(float prev, float next)
        {
            
        }

    }
}