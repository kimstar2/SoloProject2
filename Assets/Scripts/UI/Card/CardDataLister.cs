using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace UI.Card
{
    public class CardDataLister : MonoBehaviour
    {
        public List<ICardDataProvider> CardDataList { get; private set; } = new();
        private void Awake()
        {
            CardDataList.AddRange(GetComponentsInChildren<ICardDataProvider>());
        }
    }
}