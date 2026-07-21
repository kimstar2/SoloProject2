using Interface;
using UnityEngine;

namespace Object
{
    public class TestDamageableObject : MonoBehaviour, IDamageable
    {
        public void TakeDamage(float damage)
        {
            Debug.Log(damage);
        }
    }
}