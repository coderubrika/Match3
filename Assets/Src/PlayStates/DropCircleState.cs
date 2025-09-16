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
            CircleObject circleObject = pool.Spawn();
            circleObject.transform.SetParent(session.Context.GameRoot);
            session.Context.AddSpawnedCircle(circleObject);
            circleObject.gameObject.SetActive(true);
            session.Context.Pendulum.SetupOtherCircle(circleObject);
            
            session.Context.SetWaitCircleOnField(new Timer(
                updateSource,
                () =>
                {
                    pool.Despawn(session.Context.LastSpawned);
                    session.WaitTouch();
                }, 
                4));
            
            session.Context.Field.OnNext.AddListener(session.WaitTouch);
            session.Context.Field.OnFinish.AddListener(session.Finish);
        }
    }
}