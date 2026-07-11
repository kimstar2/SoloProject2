using System.Collections.Generic;
using DG.Tweening;
using SO;
using Structure;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace UI
{
    public class OnMouseAction : ForRect, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TransformActionOptionSo option;
        private string _id;

        public void OnPointerEnter(PointerEventData eventData) => OnEnterAction();
        public void OnPointerExit(PointerEventData eventData) => OnExitAction();

        protected override void Awake()
        {
            base.Awake();
            _id = GetType().Name + transform.GetInstanceID();
        }

        private void OnEnterAction()
        {
            int r = Random.Range(0, option.onActions.Count);
            TransformAction crt = option.onActions[r];
            GoAction(crt);
        }

        private void OnExitAction()
        {
            int r = Random.Range(0, option.offActions.Count);
            TransformAction crt = option.offActions[r];
            GoAction(crt);
        }

        private void GoAction(TransformAction crt)
        {
            DOTween.Kill(_id);
            Rect.DOScale(crt.Scale, crt.scaleDur)
                .SetEase(crt.scaleEase)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable)
                .SetId(_id);

            Rect.DORotate(crt.Rotation, crt.rotDur)
                .SetEase(crt.rotEase)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable)
                .SetId(_id);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (option.onActions.Count == 0) Debug.LogError("OnAction의 옵션이 비어있습니다.");
            if (option.offActions.Count == 0) Debug.LogError("OffAction의 옵션이 비어있습니다.");
        }
#endif
    }
}