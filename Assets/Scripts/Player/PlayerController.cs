using Agent;
using Interface;
using Module;
using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerController : AbstractAgent , ICardDropTarget
    {
        private InputModule _input;
        private PlayerMovementModule _mover;

        protected override void Awake()
        {
            base.Awake();
            _input = GetModule<InputModule>();
            _mover = GetModule<PlayerMovementModule>();
            
            foreach (IInitType init in GetComponentsInChildren<IInitType>())
                init.Init(this);
        }

        private void OnEnable()
        {
            _input.Input.OnMoveInputChanged += _mover.SetMoveDir;
            _input.Input.OnDashEvent += _mover.Dash;
        }

        private void OnDisable()
        {
            _input.Input.OnMoveInputChanged -= _mover.SetMoveDir;
            _input.Input.OnDashEvent -= _mover.Dash;
        }

        public bool CanReceive(ICard card)
        {
            return true; //Test
        }

        public void ReceiveCard(ICard card)
        {
            Debug.Log($"I ReceiveCard form {card}");
        }
    }
}

