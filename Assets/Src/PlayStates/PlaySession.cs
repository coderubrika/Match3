using System;

namespace Test3.PlayStates
{
    public class PlaySession
    {
        private readonly StateFactory<IPlayState> playStatesFactory;
        private IPlayState state;
        public PlayContext Context { get; }
        
        public PlaySession(PlayContext context)
        {
            Context = context;
            playStatesFactory = ServiceLocator.Instance.Get<StateFactory<IPlayState>>();
            state = playStatesFactory.Get<InitialState>();
            state.Apply(this);
        }

        public void WaitTouch()
        {
            Type stateType = state.GetType();   
            if (stateType != typeof(InitialState) && stateType != typeof(DropCircleState))
                return;
            
            state = playStatesFactory.Get<WaitTouchState>();
            state.Apply(this);
        }

        public void Touch()
        {
            if (state.GetType() != typeof(WaitTouchState))
                return;
            
            state = playStatesFactory.Get<DropCircleState>();
            state.Apply(this);
        }

        public void Finish()
        {
            if (state.GetType() != typeof(DropCircleState))
                return;
            
            state = playStatesFactory.Get<FinishState>();
            state.Apply(this);
        }
    }
}