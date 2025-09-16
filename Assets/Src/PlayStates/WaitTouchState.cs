using UnityEngine;

namespace Test3.PlayStates
{
    public class WaitTouchState : IPlayState
    {
        public void Apply(PlaySession session)
        {
            session.Context.Field.OnNext.RemoveAllListeners();
            session.Context.WaitCircleOnField?.Dispose();
            session.Context.Pendulum.SetColor((CircleColor)Random.Range(1, 4));
        }
    }
}