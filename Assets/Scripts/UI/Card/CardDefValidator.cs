using Interface;
using SO.Event;
using TMPro;
using UnityEngine;

namespace UI.Card
{
    public class CardDefValidator : MonoBehaviour , IEventer
    {
        [SerializeField] private CardValidateEventChannel cardValidateChannel;
        [SerializeField] private TextMeshProUGUI descT;
        [SerializeField] private TextMeshProUGUI statTypeT;
        [SerializeField] private TextMeshProUGUI addValT;
        [SerializeField] private TextMeshProUGUI multiValT;
        
        public void Validate(Card card)
        {
            descT.SetText(card.CardDefinition.cardDescription);
            statTypeT.SetText(string.Empty);
            addValT.SetText(string.Empty);
            multiValT.SetText(string.Empty);
            string typeT = "";
            string addT = "";
            string multiT = "";
            ICardDataProvider[] datas = card.GetApplyData();
            for (int i = 0; i < datas.Length; i++)
            {
                typeT += datas[i].CardData.statType;
                addT += datas[i].CardData.statAddValue;
                multiT += datas[i].CardData.statMultiValue;

                if (i < datas.Length - 1)
                {
                    typeT += " , ";
                    addT += " , ";
                    multiT += " , ";
                }
            }
            statTypeT.SetText($"Types : {typeT}");
            addValT.SetText($"Add : {addT}");
            multiValT.SetText($"Multi : {multiT}");
        }

        private void OnEnable() => EnableEvent();
        private void OnDisable() => DisableEvent();

        public void EnableEvent()
        {
            cardValidateChannel.OnEvent += Validate;
        }

        public void DisableEvent()
        {
            cardValidateChannel.OnEvent -= Validate;
        }
    }
}