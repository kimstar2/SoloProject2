using System.Collections.Generic;
using Interface;
using SO;
using SO.Event.Func;
using SO.HitEffect;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Card
{
    public class DeckProjectileEffectProvider : MonoBehaviour
    {
        [field: SerializeField]
        [field: FormerlySerializedAs("<ProjectileHitEffectFunc>k__BackingField")]
        public ProjectileHitEffectQueryChannel QueryChannel { get; private set; }

        private void OnEnable()
        {
            if (QueryChannel != null)
                QueryChannel.OnEvent += GetActiveEffects;
        }

        private void OnDisable()
        {
            if (QueryChannel != null)
                QueryChannel.OnEvent -= GetActiveEffects;
        }

        public IEnumerable<HitEffectSo> GetActiveEffects()
        {
            foreach (ICard card in GetComponentsInChildren<ICard>())
            {
                if (!card.IsInDeck)
                    continue;

                if (card.CardDefinition == null)
                    continue;

                foreach (HitEffectSo effect in card.CardDefinition.ProjectileHitEffects)
                {
                    if (effect != null)
                        yield return effect;
                }
            }
        }  
        
    }
}
