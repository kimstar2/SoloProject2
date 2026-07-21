using System;
using Interface;
using Interface.Marker;
using SO.CasterData;
using UnityEngine;

namespace Module.Caster
{
    public abstract class Caster : MonoBehaviour , IModule
    {
        [field:SerializeField] public Transform TargetTransform {get; private set;}
        
        [Header("Values Setting")]
        [SerializeField] protected Vector2 offset;
        [SerializeField] protected ContactFilter2D targetFilter;
        [SerializeField] protected int targetMaxAmount;
        [SerializeField] private bool castAlone;
        
        [Header("Gizmos Settings")]
        [SerializeField] protected Color gizmoColor = Color.red;
        protected Vector2 CenterPos => TargetTransform != null
            ? TargetTransform.TransformPoint(offset)
            : transform.TransformPoint(offset);
        
        protected Collider2D[] HitsResult;
        protected int HitCount;
        
        private ICastable[] _casters;
        public event Action OnCastEvent;
        protected void Update()
        {
            if (castAlone)
                Cast();
        }

        protected virtual void Awake()
        {
            _casters = GetComponentsInChildren<ICastable>();
            HitsResult = new Collider2D[Mathf.Max(1, targetMaxAmount)];
        }
        
        protected void ForceCast(int hitCount)
        {
            foreach (ICastable caster in _casters)
            {
                for (int i = 0 ; i < hitCount ; i++)
                {
                    if (HitsResult[i] != null)
                        caster?.Cast(HitsResult[i]);
                }
            }
        }
        
        public virtual void Cast()
        {
            OnCastEvent?.Invoke();
        }

        public void SendCasterData(CasterDataSo data)
        {
            foreach (ICastable caster in _casters)
                caster.HandleSetData(data);
        }

        public abstract void MultiplyScale(float multiplier = 1f);
        public abstract void SetScaleMultiplier(float multi = 1f);
        public abstract void AddScale(float value);
        public abstract void SetScaleAdd(float value);

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            targetMaxAmount = Mathf.Max(1, targetMaxAmount);
        }
#endif
        
    }
}
