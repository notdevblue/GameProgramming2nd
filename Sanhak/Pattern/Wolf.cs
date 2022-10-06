namespace Enemy
{
    public class Wolf : IEnemy
    {
        private WolfController m_controller;
        private Coord m_coord;

        public Wolf(WolfController _controller, Coord _coord)
        {
            m_controller = _controller;
            m_coord      = _coord;

            m_controller.AddListener(Spawn);
            m_controller.AddListener(MoveTo);
        }

        public void Spawn()
        {
            // 스폰
        }
        
        public void Attack()
        {
            // 공격
        }

        public void Move()
        {
            // 이동
        }

        public void MoveTo(Coord _coord)
        {
            // 해당 좌표로 이동
        }
        
        public void Damage(int _damage)
        {
            // 공격 당했을 때
        }

        public void Die()
        {
            // 사망
            m_controller.WolfDied(m_coord);
        }

    };
}