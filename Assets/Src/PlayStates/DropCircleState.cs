namespace Test3.PlayStates
{
    public class DropCircleState : IPlayState
    {
        private readonly MonoPool<CircleObject> pool;
        private readonly UpdateSource updateSource;
        
        public DropCircleState()
        {
            pool = ServiceLocator.Instance.Get<MonoPool<CircleObject>>();
            updateSource = ServiceLocator.Instance.Get<UpdateSource>();
        }
        
        public void Apply(PlaySession session)
        {
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
                session.Context.RemoveUnit(units.Item1);
                session.Context.RemoveUnit(units.Item2);
                session.Context.RemoveUnit(units.Item3);
                
                pool.Despawn(units.Item1);
                pool.Despawn(units.Item2);
                pool.Despawn(units.Item3);
            });
        }
    }
}