using Interface;
using SO;
using UnityEngine;

namespace Module
{
    public class StatDataProvider : MonoBehaviour , IStatProvider
    {
        [field:SerializeField] public StatDataSo StatData { get; private set; }
    }
}