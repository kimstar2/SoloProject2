using System;
using System.Collections.Generic;
using DG.Tweening;
using SO;
using Struct;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Utility;
using Random = UnityEngine.Random;

namespace UI
{
    public class OnMouseAction : ForRect, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent onEvent;
        public UnityEvent offEvent;
        [SerializeField] private TransformActionOptionSo option;

        private string _tweenId;

        protected override void Awake()
        {
            base.Awake();
            _tweenId = $"{GetType().Name}_{GetInstanceID()}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!TryGetRandomAction(option?.onActions, out TransformAction action)) return;

            Play(action);
            onEvent?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!TryGetRandomAction(option?.offActions, out TransformAction action)) return;

            Play(action);
            offEvent?.Invoke();
        }

        private void Play(TransformAction action)
        {
            KillTween();

            Vector3 scaleTarget = action.GetScaleTarget(Rect.localScale);
            Vector3 rotationTarget = action.GetRotationTarget(Rect.localEulerAngles);

            Sequence sequence = DOTween.Sequence()
                .SetId(_tweenId)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);

            sequence.Join(Rect.DOScale(scaleTarget, action.scaleDur).SetEase(action.scaleEase));
            sequence.Join(Rect.DOLocalRotate(rotationTarget, action.rotDur).SetEase(action.rotEase));
        }

        public void KillTween()
        {
            if (!string.IsNullOrEmpty(_tweenId))
                DOTween.Kill(_tweenId);
        }

        private static bool TryGetRandomAction(
            IReadOnlyList<TransformAction> actions,
            out TransformAction action)
        {
            if (actions == null || actions.Count == 0)
            {
                action = default;
                return false;
            }

            action = actions[Random.Range(0, actions.Count)];
            return true;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (option == null) return;
            if (option.onActions.Count == 0) Debug.LogWarning("On actions list is empty.", this);
            if (option.offActions.Count == 0) Debug.LogWarning("Off actions list is empty.", this);
        }
#endif
    }
}
