using System;

namespace Test3.PlayStates
{
    public class PlaySession
    {
        private readonly StateFactory<IPlayState> playStatesFactory;
        private readonly StateRouter<IPlayState> router;
        private readonly PlayContext context;
        
        private IPlayState state;
        
        public PlaySession(PlayContext context)
        {
            this.context = context;
            playStatesFactory = ServiceLocator.Instance.Get<StateFactory<IPlayState>>();
            router = new StateRouter<IPlayState>(ChangeState, playStatesFactory);
            router.GoTo<InitialState>();
        }

        public void Touch()
        {
            if (state.GetType() != typeof(WaitTouchState))
                return;
            
            router.GoTo<DropCircleState>();
        }
        
        private void ChangeState(IPlayState newState)
        {
            state = newState;
            state.Apply(router, context);
        }
    }
}