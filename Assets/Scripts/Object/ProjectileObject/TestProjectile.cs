namespace Object.ProjectileObject
{
    public class TestProjectile : Projectile
    {
        protected override void Awake()
        {
            base.Awake();
            Invoke(nameof(DestroyObject), 5f);
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}