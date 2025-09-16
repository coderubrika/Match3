using System;
using System.Collections.Generic;

namespace Test3.PlayStates
{
    public class PlayStatesFactory
    {
        private readonly Dictionary<Type, IPlayState> states = new();

        public TState Get<TState>()
            where TState : IPlayState, new()
        {
            Type typeState = typeof(TState);
            if (states.TryGetValue(typeState, out var state))
                return (TState)state;

            TState newState = new TState();
            states.Add(typeState, newState);
            return newState;
        }
    }
}