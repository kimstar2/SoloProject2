using UnityEngine;

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