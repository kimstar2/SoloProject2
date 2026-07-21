using Enum;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class StatUI : MonoBehaviour
    {
        private TextMeshProUGUI _statText;
        private StatType _myStat;

        private void Awake()
        {
            _statText = GetComponent<TextMeshProUGUI>();
        }

        public void SetType(StatType type) => _myStat = type;
        
        public void SetText(float prev, float next)
        {
            _statText.SetText($"{_myStat} : {next:0.##}");
        }
    }
}
