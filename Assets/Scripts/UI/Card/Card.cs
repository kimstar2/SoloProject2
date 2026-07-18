using System;
using DG.Tweening;
using Interface;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using Utility;

namespace UI.Card
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Card : ForRect, ICard, IEndDragHandler, IDragHandler, IBeginDragHandler, IDraggable
    {
        [SerializeField] private float layoutDuration = 0.25f;
        [SerializeField] private Ease layoutEase = Ease.OutCubic;

        private Sequence _layoutSequence;
        
        public bool IsDragging { get; private set; }
        public bool CanDrag { get; private set; } = true;
        public bool DropSucceeded { get; private set; }
        public bool IsInDeck => CanDrag && !DropSucceeded;
        
        private RectTransform _parentRect;
        private Vector2 _pointerOffset;
        
        public event Action OnBeginDragEvent;
        public event Action OnDragEvent;
        public event Action<bool> OnEndDragEvent;
        public event Action OnDropSuccessEvent;


        public void SetLayout(Vector2 pos, float zRot)
        {
            KillSeq();
            if (DropSucceeded || !CanDrag) return;
            
            _layoutSequence = DOTween.Sequence();

            if (!IsDragging)
            {
                _layoutSequence.Join(
                    Rect.DOAnchorPos(pos, layoutDuration)
                        .SetEase(layoutEase));
            }

            _layoutSequence.Join(
                Rect
                    .DOLocalRotate(
                        new Vector3(0f, 0f, zRot),
                        layoutDuration)
                    .SetEase(layoutEase));

            _layoutSequence.SetLink(
                gameObject,
                LinkBehaviour.KillOnDestroy);
        }

        public ICardDataProvider[] GetData() => GetComponentsInChildren<ICardDataProvider>();

        # region Drag
        
        public void OnBeginDrag(PointerEventData eventData) // Start
        {
            if (!CanDrag) return;
            if (IsDragging) return;
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            KillSeq();
            
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
            
            KillSeq();
            
            OnEndDragEvent?.Invoke(DropSucceeded);

            if (DropSucceeded)
            {
                CanDrag = false;
                OnDropSuccessEvent?.Invoke();
            }
        }
        
        #endregion Drag
        
        public void KillSeq()
        {
            _layoutSequence?.Kill();
            _layoutSequence = null;
        }
        
        public void MarkDropSucceeded()
        {
            if (!IsDragging) return;
            DropSucceeded = true;
        }
    }
}
