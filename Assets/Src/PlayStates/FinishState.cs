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
        
        public void Apply(StateRouter<IPlayState> router, PlayContext context)
        {
            context.Field.OnFinish.RemoveAllListeners();
            context.Field.OnNext.RemoveAllListeners();
            context.Field.gameObject.SetActive(false);
            
            context.Pendulum.gameObject.SetActive(false);
            context.WaitCircleOnField?.Dispose();
            
            context.SetWaitFinishAnimation(new Timer(updateSource, () =>
            {
                context.WaitFinishAnimation?.Dispose();

                foreach (var unit in context.SpawnedUnits)
                    pool.Despawn(unit);
                
                context.BroadcastFinish();
            }, 2f));
            
            
        }
    }
}