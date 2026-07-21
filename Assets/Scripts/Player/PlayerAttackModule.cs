using System.Collections.Generic;
using Interface;
using SO;
using SO.Event.Func;
using SO.HitEffect;
using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerAttackModule : TypeInit<PlayerController>, IPressedAttackable
    {
        [SerializeField] private ProjectileHitEffectQueryChannel projectileHitEffectQuery;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float attackInterval = 0.5f;

        private bool _isAttackPressed;
        private float _nextAttackTime;

        public void Attack(bool isPressed)
        {
            _isAttackPressed = isPressed;

            // 처음 누른 순간 바로 한 발 시도
            if (isPressed)
                TryAttack();
        }

        private void Update()
        {
            RotateToMouse();
            if (!_isAttackPressed) return;
            
            TryAttack();
        }
        
        private void TryAttack()
        {
            if (Time.time < _nextAttackTime) return;
            if (projectilePrefab == null || firePoint == null) return;

            _nextAttackTime = Time.time + attackInterval;
            GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            if (!projectileObject.TryGetComponent(out IProjectile projectile))
            {
                Debug.LogError("Projectile prefab must implement IProjectile.", projectileObject);
                Destroy(projectileObject);
                return;
            }

            ApplyDeckEffects(projectileObject);
            projectile.InitAndFire(firePoint.position, firePoint.rotation);
        }
        
        private void ApplyDeckEffects(GameObject projectileObject)
        {
            if (projectileHitEffectQuery == null) return;
            
            IEnumerable<HitEffectSo> effects = projectileHitEffectQuery.RaiseEvent();
            if (effects == null) return;

            IHitEffectReceiver[] receivers = projectileObject.GetComponentsInChildren<IHitEffectReceiver>();
            foreach (HitEffectSo effect in effects)
            {
                foreach (IHitEffectReceiver receiver in receivers)
                    receiver.AddHitEffect(effect);
            }
        }
        
        private void RotateToMouse()
        {
            Vector2 mouseWorldPos = Owner.GetMouseWorldPos();
            Vector2 direction = mouseWorldPos - (Vector2)transform.position;

            if (direction.sqrMagnitude <= Mathf.Epsilon) return;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            attackInterval = Mathf.Max(0.01f, attackInterval);
        }
#endif
    }
}
