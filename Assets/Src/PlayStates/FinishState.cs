namespace Test3.PlayStates
{
    public class FinishState : IPlayState
    {
        private readonly UpdateSource updateSource;
        public readonly MonoPool<CircleObject> pool;
        
        public FinishState()
        {
            updateSource = ServiceLocator.Instance.Get<UpdateSource>();
            pool = ServiceLocator.Instance.Get<MonoPool<CircleObject>>();
        }
        
        public void Apply(PlaySession session)
        {
            session.Context.Field.OnFinish.RemoveAllListeners();
            session.Context.Field.OnNext.RemoveAllListeners();
            session.Context.Field.gameObject.SetActive(false);
            
            session.Context.Pendulum.gameObject.SetActive(false);
            session.Context.WaitCircleOnField?.Dispose();
            
            session.Context.SetWaitFinishAnimation(new Timer(updateSource, () =>
            {
                session.Context.WaitFinishAnimation?.Dispose();

                foreach (var unit in session.Context.SpawnedUnits)
                    pool.Despawn(unit);
                
                session.Context.BroadcastFinish();
            }, 2f));
            
            
        }
    }
}