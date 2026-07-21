using Interface;
using Module;
using SO.Event;
using UnityEngine;
using Utility;

namespace Player
{
    [RequireComponent(typeof(CardDropReceiver))]
    public class PlayerCardReceiver : TypeInit<PlayerController>, ICardDropTarget
    {
        [field:SerializeField] public CardApplyEventChannel CardApplyChannel {get; private set;}
        
        #region ICardDropTarget

        public bool CanReceive(ICard card)
        {
            return Owner != null && Owner.PlayerStatDataLister.CanApplyCard(card);
        }

        public bool ReceiveCard(ICard card)
        {
            Owner.PlayerStatDataLister.RefreshDeckModifiers();
            bool applied = Owner.PlayerStatDataLister.ApplyPermanentCard(card);

            if (!applied)
                return false;

            CardApplyChannel?.RaiseEvent(gameObject, card);

            return true;
        }

        #endregion ICardDropTarget    
    }
}
