using Agent;
using Interface;
using Interface.Marker;
using Module;
using RunTimeData;
using SO.Event;
using UnityEngine;

namespace Player
{
    public class PlayerController : AbstractAgent , ICardDropTarget , IInitializer
    {
        private PlayerMovementModule _mover;
        private InputModule _input;
        private StatDataLister _statDataLister;
        
        [SerializeField] private CardApplyEventChannel cardApplyChannel;

        protected override void Awake()
        {
            base.Awake();

            _mover = GetModule<PlayerMovementModule>();
            _input = GetModule<InputModule>();
            _statDataLister = GetModule<StatDataLister>();
        }

        private void OnEnable()
        {
            _input.OnMovementChannel.OnEvent += _mover.SetMoveDir;
            _input.OnDashChannel.OnEvent += HandleDashInput;
        }
        
        private void OnDisable()
        {
            _input.OnMovementChannel.OnEvent -= _mover.SetMoveDir;
            _input.OnDashChannel.OnEvent -= HandleDashInput;
        }

        public bool CanReceive(ICard card)
        {
            return true;
        }

        public bool ReceiveCard(ICard card)
        {
            bool applied = _statDataLister.ApplyCard(card);

            if (!applied)
                return false;

            cardApplyChannel?.RaiseEvent(gameObject, card);

            return true;
        }
        
        private void HandleDashInput(bool pressed)
        {
            if (!pressed) return;

            bool succeeded = _mover.TryDash();

            if (!succeeded) return;
        }
    }
}

