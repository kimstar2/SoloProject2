using UnityEngine;

namespace Module.Caster
{
    public class BoxCaster : Caster
    {
        [field:SerializeField] public Transform TargetAngleZ { get; private set; }
        [field:SerializeField] public Vector2 OriginBoxSize {get; private set;}
        public Vector2 DetectBoxSize {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            DetectBoxSize = OriginBoxSize;
        }

        public override void Cast()
        {
            base.Cast();
            HitCount = Physics2D.OverlapBox(CenterPos, DetectBoxSize, TargetAngleZ.eulerAngles.z, targetFilter, HitsResult);
            ForceCast(HitCount);
        }

        public override void MultiplyScale(float multiplier = 1f) => DetectBoxSize *= multiplier;
        public override void SetScaleMultiplier(float multi = 1) => DetectBoxSize = OriginBoxSize * multi;
        public override void AddScale(float value) => DetectBoxSize += Vector2.one * value;
        public override void SetScaleAdd(float value) => DetectBoxSize = OriginBoxSize + Vector2.one * value;


#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            if (TargetAngleZ == null) return;

            Gizmos.color = gizmoColor;

            Matrix4x4 runTimeMatrix = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(
                CenterPos,
                Quaternion.Euler(0f, 0f, TargetAngleZ.eulerAngles.z),
                Vector3.one);

            Gizmos.DrawWireCube(Vector3.zero, DetectBoxSize);

            Gizmos.matrix = runTimeMatrix;
            
            Gizmos.color = Color.black;

            Matrix4x4 originMatrix = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(
                CenterPos,
                Quaternion.Euler(0f, 0f, TargetAngleZ.eulerAngles.z),
                Vector3.one);

            Gizmos.DrawWireCube(Vector3.zero, OriginBoxSize);

            Gizmos.matrix = originMatrix;
        }
        
        #endif
    }
}
