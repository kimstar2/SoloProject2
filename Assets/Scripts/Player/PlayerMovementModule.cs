using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface;
using Module;
using SO;
using UnityEngine;

namespace Player
{
    public class PlayerMovementModule : AbstractMovementModule , IDash
    {
        [Header("Dash")]
        [field:SerializeField] public DashDataSo DashData { get; private set; }
        private float _crtMulti = 0f;
        private bool _canDash = true;
        public bool IsDash { get; private set; }
        
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
            _crtMulti = DashData.dashMulti;
            DOTween.To(() => _crtMulti
                    , x => _crtMulti = x
                    , 1
                    , DashData.dashDur)
                .OnComplete(() => IsDash = false)
                .OnKill(() => IsDash = false)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        private async UniTask DashCooldown(CancellationToken token) // 대쉬 쿨타임
        {
            _canDash = false;
            await UniTask.Delay(TimeSpan.FromSeconds(DashData.dashDur + DashData.dashCool) , cancellationToken: token);
            _canDash = true;
        }
        
        # endregion Dash
    }
}