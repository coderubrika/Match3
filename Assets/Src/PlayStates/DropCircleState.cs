namespace Test3.PlayStates
{
    public class DropCircleState : IPlayState
    {
        private readonly MonoPool<CircleObject> pool;
        private readonly MonoPool<ParticleSystemWrapper> particlesPool;
        private readonly UpdateSource updateSource;
        private readonly float dropTime;
        
        public DropCircleState()
        {
            GameConfig gameConfig = ServiceLocator.Instance.Get<GameConfig>();
            dropTime = gameConfig.DropTime;
            pool = ServiceLocator.Instance.Get<MonoPool<CircleObject>>();
            particlesPool = ServiceLocator.Instance.Get<MonoPool<ParticleSystemWrapper>>();
            updateSource = ServiceLocator.Instance.Get<UpdateSource>();
        }
        
        public void Apply(StateRouter<IPlayState> router, PlayContext context)
        {
            context.ClearParticleDisposables();
            CircleObject unit = pool.Spawn();
            unit.transform.SetParent(context.GameRoot);
            context.AddSpawnedUnit(unit);
            unit.gameObject.SetActive(true);
            context.Pendulum.SetupOtherCircle(unit);
            
            context.SetWaitCircleOnField(new Timer(
                updateSource,
                () =>
                {
                    pool.Despawn(context.LastUnit);
                    context.ClearLastUnit();
                    router.GoTo<WaitTouchState>();
                }, 
                dropTime));
            
            context.Field.OnNext.AddListener(router.GoTo<WaitTouchState>);
            context.Field.OnFinish.AddListener(router.GoTo<FinishState>);
            context.Field.OnExclude.AddListener(units =>
            {
                RemoveUnit(context, units.Item1);
                RemoveUnit(context, units.Item2);
                RemoveUnit(context, units.Item3);
            });
        }

        private void RemoveUnit(PlayContext context, CircleObject unit)
        {
            var particle = particlesPool.Spawn();
            particle.SetColor(unit.Color);
            particle.transform.position = unit.transform.position;
            particle.gameObject.SetActive(true);
            context.AddParticleToDisposable(
                new PlayerHandler<ParticleSystemWrapper>(
                    updateSource, 
                    particle, 
                    p => particlesPool.Despawn(p)));
            context.RemoveUnit(unit);
            pool.Despawn(unit);
            
        }
    }
}