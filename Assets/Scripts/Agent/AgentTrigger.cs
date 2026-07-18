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

        #region MyRegion

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
            Gizmos.color = gizmoColor;
            switch (myCollider)
            {
                case BoxCollider2D:
                    Gizmos.DrawCube(transform.position, transform.localScale);
                    break;
                case CircleCollider2D:
                    Gizmos.DrawSphere(transform.position, transform.localScale.x);
                    break;
            }
        }
#endif
    }
}