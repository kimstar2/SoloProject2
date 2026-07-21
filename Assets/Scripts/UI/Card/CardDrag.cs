using System;
using Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace UI.Card
{
    public class CardDrag : ForRect , IEndDragHandler, IDragHandler, IBeginDragHandler, IDraggable
    {
        private RectTransform _parentRect;
        private Vector2 _pointerOffset;

        public bool IsDragging { get; private set; }
        public bool CanDrag { get; private set; } = true;   
        public bool DropSucceeded { get; private set; }
        
        public event Action OnBeginDragEvent;
        public event Action OnDragEvent;
        public event Action<bool> OnEndDragEvent;
        public event Action OnDropSuccessEvent;
        
        # region Drag
        
        public void OnBeginDrag(PointerEventData eventData) // Start
        {
            if (!CanDrag) return;
            if (IsDragging) return;
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            _parentRect = Rect.parent as RectTransform;

            bool success = RectTransformUtilityPlus.ScreenToLocalPos(_parentRect,eventData.position, eventData.pressEventCamera, out var localPos);
            
            if (!success) return;
            
            _pointerOffset = Rect.anchoredPosition - localPos;
            
            DropSucceeded = false;
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
            OnDragEvent?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData) // End
        {
            if (!IsDragging) return;
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            IsDragging = false;
            _parentRect = null;
            
            
            OnEndDragEvent?.Invoke(DropSucceeded);

            if (DropSucceeded)
            {
                CanDrag = false;
                OnDropSuccessEvent?.Invoke();
            }
        }
        
        #endregion Drag
        
        public void MarkDropSucceeded()
        {
            if (!IsDragging) return;
            DropSucceeded = true;
        }

        public void MarkDropFailed()
        {
            DropSucceeded = false;
        }
    }
}
