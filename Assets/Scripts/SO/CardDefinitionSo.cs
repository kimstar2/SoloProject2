using System.Collections.Generic;
using SO.HitEffect;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "CardDefinition", menuName = "SO/Card/Card Definition")]
    public class CardDefinitionSo : ScriptableObject
    {
        [field: SerializeField] public string DisplayName { get; private set; }
        [TextArea] public string cardDescription;
        [field: SerializeField] public List<CardDataSo> StatModifiers { get; private set; } = new();
        [field: SerializeField] public List<HitEffectSo> ProjectileHitEffects { get; private set; } = new();
    }
}