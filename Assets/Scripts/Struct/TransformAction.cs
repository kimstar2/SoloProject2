using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Struct
{
    [Serializable]
    public struct TransformAction
    {
        [FormerlySerializedAs("<Rotation>k__BackingField")]
        [SerializeField] private Vector3 rotation;
        [SerializeField] private bool addRotationX;
        [SerializeField] private bool addRotationY;
        [SerializeField] private bool addRotationZ;
        public float rotDur;
        public Ease rotEase;

        [FormerlySerializedAs("<Scale>k__BackingField")]
        [SerializeField] private Vector3 scale;
        [SerializeField] private bool addScaleX;
        [SerializeField] private bool addScaleY;
        [SerializeField] private bool addScaleZ;
        public float scaleDur;
        public Ease scaleEase;

        public Vector3 Rotation => rotation;
        public Vector3 Scale => scale;

        public Vector3 GetRotationTarget(Vector3 currentRotation)
        {
            return new Vector3(
                addRotationX ? currentRotation.x + rotation.x : rotation.x,
                addRotationY ? currentRotation.y + rotation.y : rotation.y,
                addRotationZ ? currentRotation.z + rotation.z : rotation.z);
        }

        public Vector3 GetScaleTarget(Vector3 currentScale)
        {
            return new Vector3(
                addScaleX ? currentScale.x + scale.x : scale.x,
                addScaleY ? currentScale.y + scale.y : scale.y,
                addScaleZ ? currentScale.z + scale.z : scale.z);
        }
    }
}