using Interface;
using UnityEngine;

namespace UI
{
    public class CanvasGroupSetter : MonoBehaviour , IModule
    {
        [SerializeField] private CanvasGroup myCanvas;
        
        public void SetCanvasBlock(bool block) 
        {
            if (myCanvas == null) return;
            myCanvas.blocksRaycasts = block;
        }
    }
}