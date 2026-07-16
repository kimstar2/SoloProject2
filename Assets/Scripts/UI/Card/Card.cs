using System;
using Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace UI.Card
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Card : ForRect, ICard, IEndDragHandler, IDragHandler, IBeginDragHandler, IDraggable
    {
        public bool IsDragging { get; private set; }
        public bool CanDrag { get; private set; } = true;
        
        private RectTransform _parentRect;
        private Vector2 _pointerOffset;
        private Vector2 _beforePos;
        
        public event Action OnBeginDragEvent;
        public event Action OnEndDragEvent;

        protected override void Awake()
        {
            base.Awake();
            foreach (IInitType init in GetComponentsInChildren<IInitType>())
                init.Init(this);
        }

        public void SetLayout(Vector2 pos, float zRot)
        {
            Rect.anchoredPosition = pos;
            Rect.localRotation = Quaternion.Euler(0, 0, zRot);
        }

        public ICardDataProvider[] GetData()
        {
            return GetComponentsInChildren<ICardDataProvider>();
        }

        # region Drag
        
        public void OnBeginDrag(PointerEventData eventData) // Start
        {
            if (!CanDrag) return;
            if (IsDragging) return;
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            _parentRect = Rect.parent as RectTransform;
            _beforePos = Rect.anchoredPosition;

            bool success = RectTransformUtilityPlus.ScreenToLocalPos(_parentRect,eventData.position, eventData.pressEventCamera, out var localPos);
            
            if (!success) return;
            
            _pointerOffset = Rect.anchoredPosition - localPos;
            IsDragging = true;
            
            OnBeginDragEvent?.Invoke();
        }

        public void OnDrag(PointerEventData eventData) // Update
        {
            if (_parentRect == null) return;
            if (!IsDragging) return;
            if (eventData.button != PointerEventData.InputButton.Left) return;

            bool success = RectTransformUtilityPlus.ScreenToLocalPos(_parentRect,eventData.position, eventData.pressEventCamera, out var localPos);
            
            if (!success) return;
            
            Rect.anchoredPosition = localPos + _pointerOffset;
        }

        public void OnEndDrag(PointerEventData eventData) // End
        {
            if (!IsDragging) return;
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            OnEndDragEvent?.Invoke();

            Rect.anchoredPosition = _beforePos;

            IsDragging = false;
            _parentRect = null;
            
        }
        
        #endregion Drag
    }
}
