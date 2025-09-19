using UnityEngine;

namespace Test3.PlayStates
{
    public class InitialState : IPlayState
    {
        private readonly GameConfig gameConfig;
        
        public InitialState()
        {
            gameConfig = ServiceLocator.Instance.Get<GameConfig>();
        }
        
        
        public void Apply(PlaySession session)
        {
            session.Context.ClearParticleDisposables();
            session.Context.Field.Init();
            session.Context.Field.gameObject.SetActive(true);
            
            session.Context.Pendulum.gameObject.SetActive(true);
            session.Context.Pendulum.Init(
                gameConfig.PendulumDistance,
                Vector2.right * gameConfig.InitialPendulumForceValue);
            
            session.Context.Pendulum.SetColor((CircleColor)Random.Range(1, 4));
            
            session.WaitTouch();
        }
    }
}