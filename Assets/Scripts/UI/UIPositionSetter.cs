using UnityEngine;
using Utility;

namespace UI
{
    public class UIPositionSetter : ForRect
    {
        public void SetPos()
        {
            Rect.anchoredPosition = Vector2.zero;
        }
    }
}