using Module;
using Module.UI;

namespace UI.Card
{
    public class CardEventer : ModuleCompo
    {
        private CanvasGroupSetter _canvasGroupSetter;
        private TweenSequence _sequence;
        private CardGroup _myCardGroup;
        
        private Card _myCard;

        protected override void Awake()
        {
            base.Awake();
            _myCard = GetComponentInParent<Card>();
            _myCardGroup = GetComponentInParent<CardGroup>();
        }

        private void Start()
        {
            _canvasGroupSetter = GetModule<CanvasGroupSetter>();
            _sequence = GetModule<TweenSequence>();
            
            _myCard.OnBeginDragEvent += HandleBeginDrag;
            _myCard.OnDragEvent += HandlerDrag;
            _myCard.OnEndDragEvent += HandleEndDrag;
            _myCard.OnDropSuccessEvent += HandleDropSuccess;
            
            _sequence.OnCompleted += HandleCompletedSeq;
        }


        # region Handler

        private void HandleBeginDrag()
        {
            _canvasGroupSetter.SetCanvasBlock(false);
            _myCardGroup.CardSet();
        }

        private void HandlerDrag()
        {
            _myCardGroup.UpdateCardOrder(_myCard);
        }

        private void HandleDropSuccess()
        {
            _sequence.Play();
            _canvasGroupSetter.SetCanvasBlock(false);
        }

        private void HandleEndDrag(bool succeeded)
        {
            if (succeeded) return;
            _canvasGroupSetter.SetCanvasBlock(true);
            _myCardGroup.CardSet();
        }
        
        private void HandleCompletedSeq() => Destroy(_myCard.gameObject);
        
        #endregion Handler

        private void OnDestroy()
        {
            _myCard.OnBeginDragEvent -= HandleBeginDrag;
            _myCard.OnDragEvent -= HandlerDrag;
            _myCard.OnEndDragEvent -= HandleEndDrag;
            _myCard.OnDropSuccessEvent -= HandleDropSuccess;
            
            _sequence.OnCompleted -= HandleCompletedSeq;
        }
    }
}   