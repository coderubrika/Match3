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
        
        
        public void Apply(StateRouter<IPlayState> router, PlayContext context)
        {
            context.ClearParticleDisposables();
            context.Field.Init();
            context.Field.gameObject.SetActive(true);
            
            context.Pendulum.gameObject.SetActive(true);
            context.Pendulum.Init(
                gameConfig.PendulumDistance,
                Vector2.right * gameConfig.InitialPendulumForceValue);
            
            context.Pendulum.SetColor((CircleColor)Random.Range(1, 4));
            
            router.GoTo<WaitTouchState>();
        }
    }
}