using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SO
{
    [CreateAssetMenu(fileName = "Input data", menuName = "SO/Input/Input data", order = 0)]
    public class InputSo : ScriptableObject , Control.IPlayerActions
    {
        private Control _controls;
        
        # region IPlayerActions
        public event Action OnDashEvent;
        /*============================================================================================================*/
        public event Action<bool> OnInteractKeyPressed;
        public event Action<bool> OnSprintKeyPressed;
        public event Action<bool> OnCrouchKeyPressed;
        public event Action<bool> OnAttackKeyPressed;
        # endregion IPlayerActions
        
        public Vector2 MovementInput {get; private set;}
        public Vector2 PointerInput {get; private set;}
        
        # region Life
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Control();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Enable();
        }

        private void OnDisable() => _controls?.Disable();
        # endregion Life

        # region ReadValue
        public void OnMove(InputAction.CallbackContext context) => MovementInput = context.ReadValue<Vector2>();

        public void OnLook(InputAction.CallbackContext context) => PointerInput = context.ReadValue<Vector2>();
        # endregion ReadValue

        # region Actions
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttackKeyPressed?.Invoke(true);
            if (context.canceled)
                OnAttackKeyPressed?.Invoke(false);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteractKeyPressed?.Invoke(true);
            if (context.canceled)
                OnInteractKeyPressed?.Invoke(false);
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnCrouchKeyPressed?.Invoke(true);
            if (context.canceled)
                OnCrouchKeyPressed?.Invoke(false);
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnDashEvent?.Invoke();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSprintKeyPressed?.Invoke(true);
            if (context.canceled)
                OnSprintKeyPressed?.Invoke(false);
        }
        # endregion Actions
    }
}