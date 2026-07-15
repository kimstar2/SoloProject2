using Interface;
using SO;
using UnityEngine;

namespace UI.Card
{
    public class CardDataProvider : MonoBehaviour , ICardDataProvider
    {
        [field:SerializeField] public CardDataSo CardData { get; private set; }
    }
}