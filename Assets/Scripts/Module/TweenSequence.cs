using System;
using DG.Tweening;
using Interface.Marker;
using SO;
using Struct;
using UnityEngine;

namespace Module
{
    public class TweenSequence : MonoBehaviour, IModule
    {
        [SerializeField] private GameObject targetGo;
        [SerializeField] private UpdateType updateType;
        [SerializeField] private bool independentTime;
        [SerializeField] private SequenceStepSo stepSo;
        
        private Transform _targetTransform;
        private RectTransform _targetRect;
        private CanvasGroup _targetCanvas;

        private Sequence _activeSequence;

        public event Action OnCompleted;

        public bool IsPlaying =>
            _activeSequence != null &&
            _activeSequence.IsActive() &&
            _activeSequence.IsPlaying();

        private void Awake()
        {
            CacheTarget();
        }

        private void CacheTarget()
        {
            if (targetGo == null)
                targetGo = gameObject;

            _targetTransform = targetGo.transform;
            _targetRect =
                targetGo.GetComponent<RectTransform>();

            _targetCanvas =
                targetGo.GetComponent<CanvasGroup>();
        }

        public bool Play()
        {
            Kill();

            if (stepSo == null || stepSo.steps == null || stepSo.steps.Count == 0)
            {
                Debug.LogWarning("TweenSequence has no configured steps.", this);
                return false;
            }

            _activeSequence = DOTween.Sequence();
            _activeSequence
                .SetId(GetEntityId())
                .SetUpdate(updateType, independentTime);

            bool hasTween = false;

            foreach (SequenceTweenStep step in stepSo.steps)
            {
                if (step == null)
                    continue;

                Tween tween = CreateTween(step);

                if (tween == null)
                    continue;

                tween.SetEase(step.Ease);

                ApplyRelative(tween, step);

                InsertTween(
                    _activeSequence,
                    tween,
                    step.InsertType);

                hasTween = true;
            }

            if (!hasTween)
            {
                _activeSequence.Kill();
                _activeSequence = null;
                return false;
            }

            _activeSequence
                .SetLink(
                    targetGo,
                    LinkBehaviour.KillOnDestroy)
                .OnComplete(HandleComplete);

            return true;
        }

        private Tween CreateTween(
            SequenceTweenStep step)
        {
            switch (step.ActionType)
            {
                case TweenActionType.AnchoredPosition:
                    return CreatePositionTween(step);

                case TweenActionType.LocalRotation:
                    return CreateRotationTween(step);

                case TweenActionType.LocalScale:
                    return CreateScaleTween(step);

                case TweenActionType.CanvasAlpha:
                    return CreateAlphaTween(step);

                default:
                    return null;
            }
        }

        private Tween CreatePositionTween(
            SequenceTweenStep step)
        {
            if (_targetRect == null)
            {
                Debug.LogError(
                    "AnchoredPosition에는 RectTransform이 필요합니다.",
                    this);

                return null;
            }

            Vector2 targetPosition =
                new Vector2(
                    step.VectorValue.x,
                    step.VectorValue.y);

            return _targetRect.DOAnchorPos(
                targetPosition,
                step.Duration);
        }

        private Tween CreateRotationTween(
            SequenceTweenStep step)
        {
            if (_targetTransform == null)
                return null;

            return _targetTransform.DOLocalRotate(
                step.VectorValue,
                step.Duration);
        }

        private Tween CreateScaleTween(
            SequenceTweenStep step)
        {
            if (_targetTransform == null)
                return null;

            return _targetTransform.DOScale(
                step.VectorValue,
                step.Duration);
        }

        private Tween CreateAlphaTween(
            SequenceTweenStep step)
        {
            if (_targetCanvas == null)
            {
                Debug.LogError(
                    "CanvasAlpha에는 CanvasGroup이 필요합니다.",
                    this);

                return null;
            }

            return _targetCanvas.DOFade(
                step.AlphaValue,
                step.Duration);
        }

        private void ApplyRelative(
            Tween tween,
            SequenceTweenStep step)
        {
            if (!step.Relative)
                return;

            if (step.ActionType ==
                TweenActionType.CanvasAlpha)
            {
                return;
            }

            tween.SetRelative();
        }

        private void InsertTween(
            Sequence sequence,
            Tween tween,
            SequenceInsertType insertType)
        {
            switch (insertType)
            {
                case SequenceInsertType.Append:
                    sequence.Append(tween);
                    break;

                case SequenceInsertType.Join:
                    sequence.Join(tween);
                    break;

                case SequenceInsertType.Prepend:
                    sequence.Prepend(tween);
                    break;
            }
        }

        public void Kill(bool complete = false)
        {
            if (_activeSequence == null)
                return;

            if (_activeSequence.IsActive())
                _activeSequence.Kill(complete);

            _activeSequence = null;
        }

        private void HandleComplete()
        {
            _activeSequence = null;
            OnCompleted?.Invoke();
        }

        private void OnDestroy()
        {
            Kill();
        }
    }
}
