using Interface;
using Module;

namespace UI.Card
{
    public class CardEventer : ModuleCompo , IInitType
    {
        private CanvasGroupSetter _canvasGroupSetter;
        private Card _myCard;
        
        public void Init<T>(T type)
        {
            if (type == null) return;
            _myCard = type as Card;
        }

        private void Start()
        {
            _canvasGroupSetter = GetModule<CanvasGroupSetter>();
            
            _myCard.OnBeginDragEvent += BeginDragHandler;
            _myCard.OnEndDragEvent += EndDragHandler;
        }

        private void BeginDragHandler()
        {
            _canvasGroupSetter.SetCanvasBlock(false);
        }

        private void EndDragHandler()
        {
            _canvasGroupSetter.SetCanvasBlock(true);
        }

        private void OnDestroy()
        {
            _myCard.OnBeginDragEvent -= BeginDragHandler;
            _myCard.OnEndDragEvent -= EndDragHandler;
        }
    }
}