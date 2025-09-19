namespace Test3.PlayStates
{
    public class DropCircleState : IPlayState
    {
        private readonly MonoPool<CircleObject> pool;
        private readonly MonoPool<ParticleSystemWrapper> particlesPool;
        private readonly UpdateSource updateSource;
        
        public DropCircleState()
        {
            pool = ServiceLocator.Instance.Get<MonoPool<CircleObject>>();
            particlesPool = ServiceLocator.Instance.Get<MonoPool<ParticleSystemWrapper>>();
            updateSource = ServiceLocator.Instance.Get<UpdateSource>();
        }
        
        public void Apply(PlaySession session)
        {
            session.Context.ClearParticleDisposables();
            CircleObject unit = pool.Spawn();
            unit.transform.SetParent(session.Context.GameRoot);
            session.Context.AddSpawnedUnit(unit);
            unit.gameObject.SetActive(true);
            session.Context.Pendulum.SetupOtherCircle(unit);
            
            session.Context.SetWaitCircleOnField(new Timer(
                updateSource,
                () =>
                {
                    pool.Despawn(session.Context.LastUnit);
                    session.Context.ClearLastUnit();
                    session.WaitTouch();
                }, 
                4));
            
            session.Context.Field.OnNext.AddListener(session.WaitTouch);
            session.Context.Field.OnFinish.AddListener(session.Finish);
            session.Context.Field.OnExclude.AddListener(units =>
            {
                RemoveUnit(session.Context, units.Item1);
                RemoveUnit(session.Context, units.Item2);
                RemoveUnit(session.Context, units.Item3);
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