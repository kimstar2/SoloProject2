using System;
using Interface;
using SO;
using UnityEngine;

namespace Module
{
    public class InputModule : MonoBehaviour , IInput
    {
        [field:SerializeField] public InputSo Input {get; private set;}
    }
}