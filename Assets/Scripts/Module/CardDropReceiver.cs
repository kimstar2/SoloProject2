using Interface;
using UI;
using UI.Card;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Module
{
    public class CardDropReceiver : MonoBehaviour , IDropHandler , IInitType , IModule
    {
        private ICardDropTarget _target;

        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggedGo = eventData.pointerDrag;

            if (draggedGo == null) return;
            if (!draggedGo.TryGetComponent(out Card card)) return;
            if (!_target.CanReceive(card)) return;

            _target.ReceiveCard(card);
        }

        public void Init<T>(T type) => _target = type as ICardDropTarget;
    }
}