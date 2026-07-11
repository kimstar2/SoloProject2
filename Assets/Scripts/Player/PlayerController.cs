using Agent;
using Module;

namespace Player
{
    public class PlayerController : AbstractAgent
    {
        private InputModule _inputModule;
        private MovementPlus _movementPlus;

        protected override void Awake()
        {
            base.Awake();
            _inputModule = GetModule<InputModule>();
            _movementPlus = GetModule<MovementPlus>();
        }

        private void OnEnable()
        {
            _inputModule.Input.OnMoveInputChanged += _movementPlus.SetMoveDir;
            _inputModule.Input.OnDashEvent += _movementPlus.Dash;
        }

        private void OnDisable()
        {
            _inputModule.Input.OnMoveInputChanged -= _movementPlus.SetMoveDir;
            _inputModule.Input.OnDashEvent -= _movementPlus.Dash;
        }
    }
}

