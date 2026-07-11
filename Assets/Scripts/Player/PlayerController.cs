using Agent;

namespace Player
{
    public class PlayerController : AbstractAgent
    {
        private InputModule _inputModule;

        protected override void Awake()
        {
            base.Awake();
            _inputModule = GetModule<InputModule>();
        }

        private void FixedUpdate()
        {
            MovementCompo.Move(_inputModule.Input.MovementInput);
        }
    }
}

