using UnityEngine;

namespace Utility
{
    public static class RectTransformUtilityPlus
    {
        public static bool ScreenToLocalPos(RectTransform rect , Vector3 screenPos , Camera cam, out Vector2 localPos)
        {
            return RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rect,
                screenPos,
                cam,
                out localPos);
        }
    }
}