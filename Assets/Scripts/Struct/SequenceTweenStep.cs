using System;
using DG.Tweening;
using UnityEngine;

namespace Struct
{
    public enum TweenActionType
    {
        AnchoredPosition,
        LocalRotation,
        LocalScale,
        CanvasAlpha
    }

    public enum SequenceInsertType
    {
        Append,
        Join,
        Prepend
    }

    [Serializable]
    public sealed class SequenceTweenStep
    {
        [field: SerializeField] public TweenActionType ActionType { get; private set; }
        [field: SerializeField] public SequenceInsertType InsertType { get; private set; }
        [field: SerializeField] public Vector3 VectorValue { get; private set; }

        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float AlphaValue { get; private set; }

        [field: SerializeField]
        [field: Min(0f)]
        public float Duration { get; private set; } = 0.2f;

        [field: SerializeField] public Ease Ease { get; private set; } = Ease.Linear;

        [field: SerializeField] public bool Relative { get; private set; }
    }
}
