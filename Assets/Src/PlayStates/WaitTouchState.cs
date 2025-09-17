using UnityEngine;

namespace Test3.PlayStates
{
    public class WaitTouchState : IPlayState
    {
        public void Apply(PlaySession session)
        {
            session.Context.PushLastUnit();
            session.Context.Field.OnNext.RemoveAllListeners();
            session.Context.Field.OnFinish.RemoveAllListeners();
            session.Context.Field.OnExclude.RemoveAllListeners();
            
            session.Context.WaitCircleOnField?.Dispose();
            session.Context.Pendulum.SetColor((CircleColor)Random.Range(1, 4));
        }
    }
}