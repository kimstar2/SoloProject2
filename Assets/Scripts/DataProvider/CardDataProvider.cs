using Interface;
using SO;
using UnityEngine;

namespace DataProvider
{
    public class CardDataProvider : MonoBehaviour , ICardDataProvider
    {
        [field:SerializeField] public CardDataSo CardData { get; private set; }
    }
}