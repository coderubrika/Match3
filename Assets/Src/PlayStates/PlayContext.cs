using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test3.PlayStates
{
    public class PlayContext
    {
        private readonly Stack<CircleObject> spawnedCircles = new();
        
        public IDisposable WaitCircleOnField { get; private set; }
        public IDisposable WaitFinishAnimation { get; private set; }

        public event Action OnFinish; 
        
        public Field Field { get; }
        public Pendulum Pendulum { get; }

        public Transform GameRoot { get; }
        
        public IEnumerable<CircleObject> SpawnedCircles => spawnedCircles;

        public CircleObject LastSpawned => spawnedCircles.Pop();
        
        public PlayContext(
            Field field, 
            Pendulum pendulum,
            Transform gameRoot)
        {
            Field = field;
            Pendulum = pendulum;
            GameRoot = gameRoot;
        }

        public void AddSpawnedCircle(CircleObject circleObject)
        {
            spawnedCircles.Push(circleObject);
        }

        public void SetWaitCircleOnField(IDisposable waitDisposable)
        {
            WaitCircleOnField = waitDisposable;
        }
        
        public void SetWaitFinishAnimation(IDisposable waitDisposable)
        {
            WaitFinishAnimation = waitDisposable;
        }

        public void BroadcastFinish()
        {
            OnFinish?.Invoke();
        }
    }
}