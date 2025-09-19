using UnityEngine;

namespace Test3.PlayStates
{
    public class WaitTouchState : IPlayState
    {
        public void Apply(StateRouter<IPlayState> router, PlayContext context)
        {
            context.PushLastUnit();
            context.Field.OnNext.RemoveAllListeners();
            context.Field.OnFinish.RemoveAllListeners();
            context.Field.OnExclude.RemoveAllListeners();
            
            context.WaitCircleOnField?.Dispose();
            context.Pendulum.SetColor((CircleColor)Random.Range(1, 4));
        }
    }
}