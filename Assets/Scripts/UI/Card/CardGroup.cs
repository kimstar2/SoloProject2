using System;
using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace UI.Card
{
    public class CardGroup : ForRect
    {
        [Header("Card Layout")]
        [SerializeField] private float maxHorizontalSpacing = 120f;
        [SerializeField] private float maxRotationPerCard = 8f;
        [SerializeField] private float maxTotalRotation = 40f;
        [SerializeField] private float maxArcDrop = 60f;

        private readonly List<ICard> _cards = new();
        private bool _isChangingOrder;
        public event Action OnIndexChanged;

        private void Start()
        {
            CardSet();
        }

        public void CardSet(bool includeDragging = true)
        {
            RefreshCards(includeDragging);

            for (int i = 0; i < _cards.Count; i++)
                SetCardLayout(i);
        }
        
        private void RefreshCards(bool includeDragging)
        {
            _cards.Clear();

            foreach (ICard card in GetComponentsInChildren<ICard>())
            {
                if (!card.IsInDeck)
                    continue;

                if (!includeDragging && card is IDraggable { IsDragging: true })
                    continue;

                _cards.Add(card);
            }
        }
        private void SetCardLayout(int index)
        {
            float center = (_cards.Count - 1) * 0.5f;
            float offset = index - center;
            float normalizedOffset = center > 0f ? offset / center : 0f;
            float x = offset * CalculateHorizontalSpacing();
            float y = _cards.Count == 2 ? -normalizedOffset : -normalizedOffset * normalizedOffset * maxArcDrop;
            float rotation = -offset * CalculateRotationPerCard();

            _cards[index].SetLayout(new Vector2(x, y), rotation);
        }

        public void UpdateCardOrder(Card draggedCard)
        {
            RefreshCards(true);
            List<Card> cards = new();

            foreach (ICard card in _cards)
            {
                if (card is Card concreteCard) 
                    cards.Add(concreteCard);
            }

            if (cards.Count <= 1) return;
            int currentIndex = cards.IndexOf(draggedCard);

            if (currentIndex < 0) return;
            RectTransform draggedRect = draggedCard.transform as RectTransform;

            if (draggedRect == null) return;
            if (draggedRect.anchoredPosition.y > Rect.rect.yMax)
                CardSet(false);
            else
                CardSet();
            RefreshCards(true);
            
            float spacing = CalculateHorizontalSpacing();

            if (spacing <= 0f) return;
            float center = (cards.Count - 1) * 0.5f;

            int targetIndex = Mathf.RoundToInt(draggedRect.anchoredPosition.x / spacing + center);
            targetIndex = Mathf.Clamp(targetIndex, 0, cards.Count - 1);
            
            if (targetIndex == currentIndex) return;
            int targetSiblingIndex = cards[targetIndex].transform.GetSiblingIndex();
            
            _isChangingOrder = true;
            
            draggedCard.transform.SetSiblingIndex(targetSiblingIndex);
            
            _isChangingOrder = false;
        }

        private float CalculateHorizontalSpacing()
        {
            if (_cards.Count <= 1) return 0f;

            RectTransform groupRect = transform as RectTransform;
            Component firstCard = _cards[0] as Component;
            
            RectTransform cardRect = firstCard != null ? firstCard.transform as RectTransform : null;
            
            if (groupRect == null || cardRect == null) return maxHorizontalSpacing;
            
            float availableCenterWidth = Mathf.Max(0f, groupRect.rect.width - cardRect.rect.width);
            float fittedSpacing = availableCenterWidth / (_cards.Count - 1);
            
            return Mathf.Min(maxHorizontalSpacing, fittedSpacing);
        }

        private float CalculateRotationPerCard()
        {
            if (_cards.Count <= 1) return 0f;
            float fittedRotation = maxTotalRotation / (_cards.Count - 1);
            return Mathf.Min(maxRotationPerCard, fittedRotation);
        }

        private void OnTransformChildrenChanged()
        {
            if (_isChangingOrder) return;
            CardSet();
        }
    }
}