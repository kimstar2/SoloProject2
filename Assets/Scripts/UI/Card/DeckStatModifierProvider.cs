using System.Collections.Generic;
using Interface;
using SO;
using SO.Event.Func;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Card
{
    public class DeckStatModifierProvider : MonoBehaviour
    {
        [field: SerializeField]
        [field: FormerlySerializedAs("queryChannel")]
        public StatModifierQueryChannel QueryChannel { get; private set; }

        private void OnEnable()
        {
            if (QueryChannel != null)
                QueryChannel.OnEvent += GetActiveModifiers;
        }

        private void OnDisable()
        {
            if (QueryChannel != null)
                QueryChannel.OnEvent -= GetActiveModifiers;
        }

        public IEnumerable<CardDataSo> GetActiveModifiers()
        {
            foreach (ICard card in GetComponentsInChildren<ICard>())
            {
                if (!card.IsInDeck) continue;
                if (card.CardDefinition == null) continue;

                foreach (CardDataSo modifier in card.CardDefinition.StatModifiers)
                {
                    if (modifier != null)
                        yield return modifier;
                }
            }
        }
    }
}
