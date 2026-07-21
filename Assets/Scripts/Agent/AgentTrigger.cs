using Interface;
using Interface.Marker;
using UnityEngine;

namespace Agent
{
    [RequireComponent(typeof(Collider2D))]
    public class AgentTrigger : MonoBehaviour , IModule
    {
        [SerializeField] private Collider2D myCollider;
        [SerializeField] private Color gizmoColor = Color.darkOrange;
        public bool CanTrigger { get; private set; } = true;

        public void SetCanTrigger(bool canTrigger) => CanTrigger = canTrigger;

        #region Trigger

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!CanTrigger) return;
            if (other.TryGetComponent(out IEnterTriggerable enter))
                enter.OnEnter();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!CanTrigger) return;
            if (other.TryGetComponent(out IExitTriggerable exit))
                exit.OnExit();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!CanTrigger) return;
            if (other.TryGetComponent(out IStayTriggerable stay))
                stay.OnStay();
        }
        
        #endregion Trigger
        
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            if (myCollider == null)
                myCollider = GetComponent<Collider2D>();
            if (myCollider == null) return;

            Gizmos.color = gizmoColor;
            Gizmos.DrawWireCube(myCollider.bounds.center, myCollider.bounds.size);
        }
#endif
    }
}
