using System;

namespace Enemy
{
    public class WolfController
    {
        private event Action m_onPlayerEntered;
        private event Action<Coord> m_onWolfDied;

        public WolfController()
        {
            m_onPlayerEntered = () => { };
            m_onWolfDied = _coord => { };
        }

        // 플레이어가 지정된 영역 안으로 들어올 시 호출될 것들
        public void AddListener(Action _callback)
        {
            m_onPlayerEntered += _callback;
        }

        // 늑대가 죽었을때 죽은 늑대의 죄표를 인자로 받으며 호출될 것들
        public void AddListener(Action<Coord> _callback)
        {
            m_onWolfDied += _callback;
        }

        // 플레이어가 지정된 영역 안으로 들어올 시 늑대 생성
        private void OnPlayerEntered()
        {
            m_onPlayerEntered();
        }

        // 늑대가 죽을 시 이 위치로 어그로를 끔
        public void WolfDied(Coord _coord)
        {
            m_onWolfDied(_coord);
        }
    };
}