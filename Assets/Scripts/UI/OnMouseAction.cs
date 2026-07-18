using DG.Tweening;
using SO;
using Struct;
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
            KillTween();
            Vector3 scaleTarget = crt.GetScaleTarget(Rect.localScale);
            Vector3 rotationTarget = crt.GetRotationTarget(Rect.localEulerAngles);
            
            Sequence seq = DOTween.Sequence();
            seq.Join(Rect.DOScale(scaleTarget, crt.scaleDur).SetEase(crt.scaleEase).SetId(_id+1));
            seq.Join(Rect.DOLocalRotate(rotationTarget, crt.rotDur).SetEase(crt.rotEase).SetId(_id+2));
            seq.SetLink(gameObject, LinkBehaviour.KillOnDisable).SetId(_id);
        }

        public void KillTween(bool scale = true, bool rotate = true)
        {
            if (scale && rotate)
            {
                DOTween.Kill(_id);
            }
            else if (scale)
                DOTween.Kill(_id+1);
            else if (rotate)
                DOTween.Kill(_id+2);
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