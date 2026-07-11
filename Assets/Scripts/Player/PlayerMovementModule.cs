using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface;
using Module;
using UnityEngine;

namespace Player
{
    public class PlayerMovementModule : AbstractMovementModule , IDash
    {
        [field:SerializeField] public float DashMulti { get; private set; }
        [field:SerializeField] public float DashDur { get; private set; }
        [field:SerializeField] public float DashCool { get; private set; }
        public bool IsDash { get; private set; }
        private float _crtMulti;
        private bool _canDash = true;
        
        private void FixedUpdate() {
            if (IsDash)
                Move(MoveDir * _crtMulti);
            else
                Move(MoveDir);
        }
        
        # region Dash
        
        public void Dash() // 대쉬 입력
        {
            if (!_canDash) return;
            DashLogic();
            DashCooldown(destroyCancellationToken).Forget();
        }

        private void DashLogic() // 대쉬 구현
        {
            IsDash = true;
            _crtMulti = DashMulti;
            DOTween.To(() => _crtMulti
                    , x => _crtMulti = x
                    , 1
                    , DashDur)
                .OnComplete(() => IsDash = false)
                .OnKill(() => IsDash = false)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        private async UniTask DashCooldown(CancellationToken token) // 대쉬 쿨타임
        {
            _canDash = false;
            await UniTask.Delay(TimeSpan.FromSeconds(DashDur + DashCool) , cancellationToken: token);
            _canDash = true;
        }
        
        # endregion Dash
    }
}