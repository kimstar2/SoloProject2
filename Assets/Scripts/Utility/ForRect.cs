using UnityEngine;

namespace Utility
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class ForRect : MonoBehaviour
    {
        protected RectTransform Rect;

        protected virtual void Awake()
        {
            Rect = GetComponent<RectTransform>();
        }
    }
}