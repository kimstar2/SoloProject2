using System;
using Interface;
using SO.Event;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SO
{
    [CreateAssetMenu(fileName = "Input data", menuName = "SO/Input/Input data", order = 0)]
    public class InputSo : ScriptableObject, Control.IPlayerActions
    {
        private Control _controls;
        public Vector2 MovementInput { get; private set; }
        public Vector2 PointerInput { get; private set; }

        
        # region IPlayerActions
        public BoolEventChannel onInteractKeyPressed;
        public BoolEventChannel onSprintKeyPressed;
        public BoolEventChannel onCrouchKeyPressed;
        public BoolEventChannel onAttackKeyPressed;
        public BoolEventChannel onDashEvent;
        public Vector2EventChannel onMoveInputChanged;
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
        }

        private void OnDisable() => _controls?.Disable();

        # endregion Life
        
        
        # region ReadValue
        
        public void OnLook(InputAction.CallbackContext context) => PointerInput = context.ReadValue<Vector2>();

        # endregion ReadValue
        
        
        # region Actions

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
            onMoveInputChanged?.RaiseEvent(MovementInput);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                onAttackKeyPressed?.RaiseEvent(true);
            if (context.canceled)
                onAttackKeyPressed?.RaiseEvent(false);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                onInteractKeyPressed?.RaiseEvent(true);
            if (context.canceled)
                onInteractKeyPressed?.RaiseEvent(false);
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            if (context.performed)
                onCrouchKeyPressed?.RaiseEvent(true);
            if (context.canceled)
                onCrouchKeyPressed?.RaiseEvent(false);
        }


        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
                onSprintKeyPressed?.RaiseEvent(true);
            if (context.canceled)
                onSprintKeyPressed?.RaiseEvent(false);
        }
        
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
                onDashEvent?.RaiseEvent(true);
            if (context.canceled)
                onDashEvent?.RaiseEvent(false);
        }

        # endregion Actions
    }
}