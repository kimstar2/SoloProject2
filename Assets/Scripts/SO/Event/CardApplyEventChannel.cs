using Interface;
using UnityEngine;

namespace SO.Event
{
    [CreateAssetMenu(menuName = "SO/Events/Card Apply Event Channel")]
    public class CardApplyEventChannel : AbstractEventSo<(GameObject target, ICard card)>
    {
        public void RaiseEvent(GameObject target, ICard card)
        {
            base.RaiseEvent((target, card));
        }
    }
}
