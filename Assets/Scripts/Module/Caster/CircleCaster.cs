using UnityEngine;

namespace Module.Caster
{
    public class CircleCaster : Caster
    {
        [field:SerializeField] public float OriginRadius {get; private set;}
        public float DetectRadius { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DetectRadius = OriginRadius;
        }

        public override void Cast()
        {
            base.Cast();
            HitCount = Physics2D.OverlapCircle(CenterPos, DetectRadius, targetFilter, HitsResult);
            ForceCast(HitCount);
        }
        
        public override void MultiplyScale(float multiplier = 1f) => DetectRadius *= multiplier;
        public override void SetScaleMultiplier(float multi = 1) => DetectRadius = OriginRadius * multi;
        public override void AddScale(float value) => DetectRadius += value;
        public override void SetScaleAdd(float value) => DetectRadius = OriginRadius + value;


#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(CenterPos, DetectRadius);      
            
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(CenterPos, OriginRadius);
        }

#endif
    }
}
