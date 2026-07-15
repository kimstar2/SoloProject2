using Agent;
using Interface;
using Module;

namespace Player
{
    public class PlayerController : Abstract
    {
        private InputModule _input;
        private PlayerMovementModule _mover;

        protected override void Awake()
        {
            base.Awake();
            _input = GetModule<InputModule>();
            _mover = GetModule<PlayerMovementModule>();
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
    }
}

