using System;
using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace UI
{
    public class CardGroup : MonoBehaviour
    {
        [Header("Card Layout")]
        [SerializeField] private float maxHorizontalSpacing = 120f;
        [SerializeField] private float maxRotationPerCard = 8f;
        [SerializeField] private float maxTotalRotation = 40f;
        [SerializeField] private float maxArcDrop = 60f;
        
        private readonly List<ICard> _cards = new();

        private void Start()
        {
            CardSet();
        }

        public void CardSet()
        {
            _cards.Clear();
            _cards.AddRange(GetComponentsInChildren<ICard>());

            if (_cards.Count == 0)
                return;

            float center = (_cards.Count - 1) * 0.5f;
            float horizontalSpacing = CalculateHorizontalSpacing();
            float rotationPerCard = CalculateRotationPerCard();

            for (int i = 0; i < _cards.Count; i++)
            {
                float offset = i - center;
                float normalizedOffset = center > 0f ? offset / center : 0f;

                float x = offset * horizontalSpacing;
                float y = -normalizedOffset * normalizedOffset * maxArcDrop;
                float rotation = -offset * rotationPerCard;

                _cards[i].SetLayout(
                    new Vector2(x, y),
                    rotation);
            }
        }

        private float CalculateHorizontalSpacing()
        {
            if (_cards.Count <= 1)
                return 0f;

            RectTransform groupRect = transform as RectTransform;
            Component firstCard = _cards[0] as Component;
            RectTransform cardRect = firstCard != null ? firstCard.transform as RectTransform : null;

            if (groupRect == null || cardRect == null)
                return maxHorizontalSpacing;

            float availableCenterWidth = Mathf.Max(0f, groupRect.rect.width - cardRect.rect.width);
            float fittedSpacing = availableCenterWidth / (_cards.Count - 1);

            return Mathf.Min(maxHorizontalSpacing, fittedSpacing);
        }

        private float CalculateRotationPerCard()
        {
            if (_cards.Count <= 1)
                return 0f;

            float fittedRotation = maxTotalRotation / (_cards.Count - 1);
            return Mathf.Min(maxRotationPerCard, fittedRotation);
        }
    }
}
