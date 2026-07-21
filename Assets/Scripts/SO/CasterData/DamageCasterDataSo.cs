using UnityEngine;

namespace SO.CasterData
{
    [CreateAssetMenu(fileName = "CasterData", menuName = "SO/CasterData/DamageCasterData", order = 0)]
    public class DamageCasterDataSo : CasterDataSo
    {
        public float defaultDamage;
        
        public bool usingRandomDamage;
        public float minDamage;
        public float maxDamage;
    }
}