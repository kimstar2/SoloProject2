using Interface;
using SO;
using SO.Event;
using UnityEngine;

namespace Module
{
    public class PlayerInput : MonoBehaviour , IInput
    {
        [field:SerializeField] public InputSo Input { get; private set; }

        public Vector2EventChannel OnMovementChannel => Input != null ? Input.OnMoveInputChanged : null;
        public Vector2EventChannel OnPointerChannel => Input != null ? Input.OnPointerPosChanged : null;
        public BoolEventChannel OnDashChannel => Input != null ? Input.OnDashKeyPressed : null;
        public BoolEventChannel OnAttackChannel => Input != null ? Input.OnAttackKeyPressed : null;
    }
}
