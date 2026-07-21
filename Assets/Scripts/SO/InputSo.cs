using SO.Event;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace SO
{
    [CreateAssetMenu(fileName = "Input data", menuName = "SO/Input/Input data", order = 0)]
    public class InputSo : ScriptableObject, Control.IPlayerActions
    {
        private Control _controls;
        public Vector2 MovementInput { get; private set; }
        public Vector2 PointerInput { get; private set; }

        
        # region IPlayerActions
        [field: SerializeField, FormerlySerializedAs("onInteractKeyPressed")]
        public BoolEventChannel OnInteractKeyPressed { get; private set; }
        [field: SerializeField, FormerlySerializedAs("onSprintKeyPressed")]
        public BoolEventChannel OnSprintKeyPressed { get; private set; }
        [field: SerializeField, FormerlySerializedAs("onCrouchKeyPressed")]
        public BoolEventChannel OnCrouchKeyPressed { get; private set; }
        [field: SerializeField, FormerlySerializedAs("onAttackKeyPressed")]
        public BoolEventChannel OnAttackKeyPressed { get; private set; }
        [field: SerializeField, FormerlySerializedAs("onDashKeyPressed")]
        public BoolEventChannel OnDashKeyPressed { get; private set; }
        [field: SerializeField, FormerlySerializedAs("onMoveInputChanged")]
        public Vector2EventChannel OnMoveInputChanged { get; private set; }
        [field: SerializeField, FormerlySerializedAs("onPointerPosChanged")]
        public Vector2EventChannel OnPointerPosChanged { get; private set; }
        # endregion IPlayerActions
        

        # region Life

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Control();
                _controls.Player.SetCallbacks(this);
            }

            _controls.Enable();

            if (Mouse.current != null)
                PointerInput = Mouse.current.position.ReadValue();
        }

        private void OnDisable() => _controls?.Disable();

        # endregion Life
        
        
        # region ReadValue

        public void OnPointer(InputAction.CallbackContext context)
        {
            PointerInput = context.ReadValue<Vector2>();
            OnPointerPosChanged?.RaiseEvent(PointerInput);
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
            OnMoveInputChanged?.RaiseEvent(MovementInput);
            
        }

        # endregion ReadValue
        
        
        # region Actions

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttackKeyPressed?.RaiseEvent(true);
            if (context.canceled)
                OnAttackKeyPressed?.RaiseEvent(false);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteractKeyPressed?.RaiseEvent(true);
            if (context.canceled)
                OnInteractKeyPressed?.RaiseEvent(false);
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnCrouchKeyPressed?.RaiseEvent(true);
            if (context.canceled)
                OnCrouchKeyPressed?.RaiseEvent(false);
        }


        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSprintKeyPressed?.RaiseEvent(true);
            if (context.canceled)
                OnSprintKeyPressed?.RaiseEvent(false);
        }
        
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnDashKeyPressed?.RaiseEvent(true);
            if (context.canceled)
                OnDashKeyPressed?.RaiseEvent(false);
        }

        # endregion Actions
    }
}
