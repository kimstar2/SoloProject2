
using DG.Tweening;
using Interface;
using SO;
using UnityEngine;
using Utility;

namespace UI.Card
{
    [RequireComponent(typeof(CanvasGroup))] [RequireComponent(typeof(CardDrag))]
    public class Card : ForRect, ICard
    {
        [field:SerializeField] public CardDefinitionSo CardDefinition {get; private set;}
        [SerializeField] private float layoutDuration = 0.25f;
        [SerializeField] private Ease layoutEase = Ease.OutCubic;
        private Sequence _layoutSequence;
        private IDraggable _drag;

        public bool IsDragging => _drag.IsDragging;
        public bool IsInDeck => _drag.CanDrag && !_drag.DropSucceeded;

        protected override void Awake()
        {
            base.Awake();
            _drag = GetComponent<IDraggable>();
        }
        public void SetLayout(Vector2 pos, float zRot)
        {
            KillSeq();
            if (_drag.DropSucceeded || !_drag.CanDrag) return;
            
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
        
        public void KillSeq()
        {
            _layoutSequence?.Kill();
            _layoutSequence = null;
        }

        public ICardDataProvider[] GetApplyData()
        {
            return GetComponentsInChildren<ICardDataProvider>();
        }
    }
}
