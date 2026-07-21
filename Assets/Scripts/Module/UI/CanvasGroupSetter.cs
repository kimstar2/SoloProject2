using Interface.Marker;
using UnityEngine;

namespace Module.UI
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