using Interface;
using Interface.Marker;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Module
{
    public class CardDropReceiver : MonoBehaviour , IDropHandler , IModule
    {
        private ICardDropTarget _target;

        private void Awake()
        {
            _target = GetComponent<ICardDropTarget>();

            if (_target == null)
                Debug.LogError("CardDropReceiver requires an ICardDropTarget on the same object.", this);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            GameObject draggedGo = eventData.pointerDrag;

            if (draggedGo == null) return;  
            if (!draggedGo.TryGetComponent(out ICard card)) return;
            if (!draggedGo.TryGetComponent(out IDraggable draggable)) return;

            if (_target == null) return;
            if (!_target.CanReceive(card)) return;

            draggable.MarkDropSucceeded();

            if (!_target.ReceiveCard(card))
                draggable.MarkDropFailed();
        }
    }
}
