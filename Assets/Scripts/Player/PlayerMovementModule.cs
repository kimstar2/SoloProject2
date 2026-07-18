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
    public class PlayerMovementModule : AbstractMovementModule, IDash
    {
        [Header("ReferenceData")]
        [field: SerializeField]
        public DashDataSo OriginDashData { get; private set; }
        public DashDataSo DashData { get; private set; }
        
        private bool _canDash = true;
        private float _crtMulti;
        public bool IsDash { get; private set; }


        private void Awake()
        {
            CloneData();
        }

        private void FixedUpdate()
        {
            if (IsDash)
                Move(MoveDir * _crtMulti);
            else
                Move(MoveDir);
        }

        private void OnDestroy()
        {
            DestroyData();
        }

        # region Data

        private void DestroyData()
        {
            if (DashData != null)
                Destroy(DashData);
        }

        private void CloneData()
        {
            if (OriginDashData == null)
            {
                Debug.LogError("DashData가 할당되지 않았습니다.", this);
                enabled = false;
                return;
            }

            DashData = Instantiate(OriginDashData);
        }

        # endregion Data

        # region Dash

        public bool TryDash()
        {
            if (!_canDash || DashData == null)
                return false;

            DashLogic();
            DashCooldown(destroyCancellationToken).Forget();
            return true;
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
            await UniTask.Delay(TimeSpan.FromSeconds(DashData.dashDur + DashData.dashCool), cancellationToken: token);
            _canDash = true;
        }

        # endregion Dash
    }
}