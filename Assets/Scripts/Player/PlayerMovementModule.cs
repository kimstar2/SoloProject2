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
        [Header("Dash Data")]
        [field: SerializeField] public DashDataSo OriginDashData { get; private set; }

        public DashDataSo DashData { get; private set; }
        public bool IsDash { get; private set; }

        private bool _canDash = true;
        private float _currentSpeedMultiplier = 1f;
        private Tween _dashTween;

        private void Awake()
        {
            CloneDashData();
        }

        private void FixedUpdate()
        {
            float multiplier = IsDash ? _currentSpeedMultiplier : 1f;
            Move(MoveDir * multiplier);
        }

        public bool TryDash()
        {
            if (!_canDash || DashData == null) return false;

            StartDash();
            RunDashCooldown(destroyCancellationToken).Forget();
            return true;
        }

        public void StopDash()
        {
            if (_dashTween != null && _dashTween.IsActive())
                _dashTween.Kill();

            _dashTween = null;
            IsDash = false;
            _currentSpeedMultiplier = 1f;
        }

        private void StartDash()
        {
            StopDash();

            IsDash = true;
            _currentSpeedMultiplier = DashData.dashMulti;
            _dashTween = DOTween.To(
                    () => _currentSpeedMultiplier,
                    value => _currentSpeedMultiplier = value,
                    1f,
                    DashData.dashDur)
                .OnComplete(() => IsDash = false)
                .OnKill(() => IsDash = false)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        private async UniTask RunDashCooldown(CancellationToken token)
        {
            _canDash = false;

            float cooldown = DashData.dashDur + DashData.dashCool;
            await UniTask.Delay(TimeSpan.FromSeconds(cooldown), cancellationToken: token);

            _canDash = true;
        }

        private void CloneDashData()
        {
            if (OriginDashData == null)
            {
                Debug.LogError("OriginDashData is not assigned.", this);
                enabled = false;
                return;
            }

            DashData = Instantiate(OriginDashData);
        }

        private void OnDestroy()
        {
            StopDash();

            if (DashData != null)
                Destroy(DashData);
        }
    }
}
