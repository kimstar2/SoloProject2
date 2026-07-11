using System;
using DG.Tweening;
using Interface;
using UnityEngine;

namespace Structure
{
    [Serializable]
    public struct TransformAction : IForRot , IForScale
    {
        [field:SerializeField] public Vector3 Rotation { get; private set;}
        public float rotDur;
        public Ease rotEase;
        
        [field:SerializeField] public Vector3 Scale { get; private set;}
        public float scaleDur;
        public Ease scaleEase;
    }
}