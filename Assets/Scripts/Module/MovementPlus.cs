using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface;
using UnityEngine;

namespace Module
{
    public class MovementPlus : AbstractMovement , IModule , IDash
    {
        [field:SerializeField] public float DashMulti { get; private set; }
        [field:SerializeField] public float DashDur { get; private set; }
        [field:SerializeField] public float DashCool { get; private set; }
        [field:SerializeField] public Ease DashEaseType { get; private set; } = Ease.Linear;

        public bool IsDash { get; private set; }
        private float _crtMulti;
        private bool _canDash = true;

        private void FixedUpdate() {
            if (IsDash)
                Move(MoveDir * _crtMulti);
            else
                Move(MoveDir);
        }

        public void Dash()
        {
            if (!_canDash) return;
            DashLogic();
            DashCooldown(destroyCancellationToken).Forget();
        }

        private void DashLogic()
        {
            IsDash = true;
            _crtMulti = DashMulti;
            DOTween.To(() => _crtMulti
                    , x => _crtMulti = x
                    , 1
                    , DashDur)
                .SetEase(DashEaseType)
                .OnComplete(() => IsDash = false)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        private async UniTask DashCooldown(CancellationToken token)
        {
            _canDash = false;
            await UniTask.Delay(TimeSpan.FromSeconds(DashDur + DashCool) , cancellationToken: token);
            _canDash = true;
        }
    }
}