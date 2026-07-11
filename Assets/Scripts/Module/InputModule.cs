using Interface;
using SO;
using UnityEngine;

namespace Module
{
    public class InputModule : MonoBehaviour , IModule
    {
        [field:SerializeField] public InputSo Input {get; private set;}
    }
}