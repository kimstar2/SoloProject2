using UnityEngine;

namespace UI
{
    public abstract class ForRect : MonoBehaviour
    {
        protected RectTransform Rect;

        protected virtual void Awake()
        {
            Rect = GetComponent<RectTransform>();
        }
    }
}