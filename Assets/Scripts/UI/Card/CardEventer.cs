using Interface;
using Module;
using Module.UI;
using SO.Event;
using UnityEngine;

namespace UI.Card
{
    public class CardEventer : ModuleCompo, IEventer
    {
        [SerializeField] private CardValidateEventChannel cardValidateChannel;
        private Card _myCard;
        private CardDrag _cardDrag;
        private CardGroup _myCardGroup;

        private CanvasGroupSetter _canvasGroupSetter;
        private TweenSequence _sequence;
        private OnMouseAction _onMouseAction;
        private bool _eventsEnabled;

        protected override void Awake()
        {
            base.Awake();
            _myCard = GetComponentInParent<Card>();
            _cardDrag = GetComponentInParent<CardDrag>();
            _myCardGroup = GetComponentInParent<CardGroup>();
            _onMouseAction = transform.parent.GetComponentInChildren<OnMouseAction>();

            _canvasGroupSetter = GetRequiredModule<CanvasGroupSetter>();
            _sequence = GetRequiredModule<TweenSequence>();

            if (_myCard == null || _cardDrag == null || _myCardGroup == null)
                Debug.LogError("CardEventer must be placed under a Card inside a CardGroup.", this);
        }

        # region Handler

        private void HandleBeginDrag()
        {
            _canvasGroupSetter.SetCanvasBlock(false);
            _myCardGroup.CardSet();
        }

        private void HandlerDrag()
        {
            _myCard.KillSeq();
            _myCardGroup.UpdateCardOrder(_myCard);
        }

        private void HandleDropSuccess()
        {
            _myCardGroup.NotifyDeckChanged();

            _canvasGroupSetter.SetCanvasBlock(false);

            if (!_sequence.Play())
                HandleCompletedSeq();
        }

        private void HandleEndDrag(bool succeeded)
        {
            _myCard.KillSeq();
            if (succeeded) return;
            _canvasGroupSetter.SetCanvasBlock(true);
            _myCardGroup.CardSet();
        }

        private void HandleCompletedSeq() => Destroy(_myCard.gameObject);

        private void HandleConnectValidateChannel()
        {
            if (_myCardGroup.CardHasDragging()) return;
            cardValidateChannel.RaiseEvent(_myCard);
        }

        #endregion Handler

        private void OnEnable() => EnableEvent();
        private void OnDisable() => DisableEvent();

        public void EnableEvent()
        {
            if (_eventsEnabled || _cardDrag == null || _sequence == null) return;

            _cardDrag.OnBeginDragEvent += HandleBeginDrag;
            _cardDrag.OnDragEvent += HandlerDrag;
            _cardDrag.OnEndDragEvent += HandleEndDrag;
            _cardDrag.OnDropSuccessEvent += HandleDropSuccess;

            _sequence.OnCompleted += HandleCompletedSeq;
            
            _onMouseAction.onEvent.AddListener(HandleConnectValidateChannel);
            
            _eventsEnabled = true;
        }

        public void DisableEvent()
        {
            if (!_eventsEnabled || _cardDrag == null || _sequence == null) return;

            _cardDrag.OnBeginDragEvent -= HandleBeginDrag;
            _cardDrag.OnDragEvent -= HandlerDrag;
            _cardDrag.OnEndDragEvent -= HandleEndDrag;
            _cardDrag.OnDropSuccessEvent -= HandleDropSuccess;

            _sequence.OnCompleted -= HandleCompletedSeq;
            
            _onMouseAction.onEvent.RemoveListener(HandleConnectValidateChannel);
            
            _eventsEnabled = false;
        }
    }
}   
