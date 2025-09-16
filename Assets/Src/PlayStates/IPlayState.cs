using System;

namespace Test3.PlayStates
{
    public interface IPlayState
    {
        public void Apply(PlaySession session);
    }
}