namespace Enemy
{
    interface IEnemy
    {
        void Spawn();
        void Attack();
        void Move();
        void Damage(int _damage);
        void Die();
    };
}