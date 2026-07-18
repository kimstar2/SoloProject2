using Interface;
using SO;
using SO.Event;
using UnityEngine;

namespace Module
{
    public class InputModule : MonoBehaviour , IInput
    {
        [field:SerializeField] public InputSo Input { get; private set; }
        [field:SerializeField] public Vector2EventChannel OnMovementChannel {get; private set;}
        [field:SerializeField] public BoolEventChannel OnDashChannel {get; private set;}
    }
}