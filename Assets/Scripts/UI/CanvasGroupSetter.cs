using System;
using Interface;
using UnityEngine;

namespace UI
{
    public class CanvasGroupSetter : MonoBehaviour , IModule
    {
        private CanvasGroup _myCanvas;

        private void Awake() => _myCanvas = GetComponentInParent<CanvasGroup>();

        public void SetCanvasBlock(bool block)
        {
            if (_myCanvas == null) return;
            _myCanvas.blocksRaycasts = block;
        }
    }
}