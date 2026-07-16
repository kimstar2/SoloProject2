using TMPro;
using UnityEngine;

namespace DataProvider
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class StatUIProvider : MonoBehaviour
    {
        [field:SerializeField] public StatDataProvider StatData { get; private set; }
        private TextMeshProUGUI _statText;

        private void Awake()
        {
            _statText = GetComponent<TextMeshProUGUI>();
            SetText();
        }

        private void SetText()
        {
            if (StatData == null) return;
            _statText.SetText($"{StatData.StatData.statType} : {StatData.StatData.statValue}");
        }
    }
}