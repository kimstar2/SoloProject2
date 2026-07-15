using Interface;
using Module;
using UnityEngine;

namespace UI
{
    public class CardEventer : ModuleCompo , IInitType
    {
        private CanvasGroupSetter _canvasGroupSetter;
        public Card myCard;
        
        public void Init<T>(T type)
        {
            if (type == null) return;
            myCard = type as Card;
        }

        private void Start()
        {
            _canvasGroupSetter = GetModule<CanvasGroupSetter>();
            
            myCard.OnBeginDragEvent += BeginDragHandler;
            myCard.OnEndDragEvent += EndDragHandler;
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
            myCard.OnBeginDragEvent -= BeginDragHandler;
            myCard.OnEndDragEvent -= EndDragHandler;
        }
    }
}