using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test3.PlayStates
{
    public class PlayContext
    {
        private readonly HashSet<CircleObject> spawnedUnits = new();
        private readonly List<IDisposable> particleDisposables = new();
        
        public CircleObject LastUnit { get; private set; }
        
        public IDisposable WaitCircleOnField { get; private set; }
        public IDisposable WaitFinishAnimation { get; private set; }
        public event Action OnFinish; 
        
        public Field Field { get; }
        public Pendulum Pendulum { get; }

        public Transform GameRoot { get; }
        
        public int Score { get; private set; }
        
        public IEnumerable<CircleObject> SpawnedUnits => spawnedUnits;

        //public CircleObject LastSpawned => spawnedUnits.Pop();
        
        public PlayContext(
            Field field, 
            Pendulum pendulum,
            Transform gameRoot)
        {
            Field = field;
            Pendulum = pendulum;
            GameRoot = gameRoot;
        }

        public void AddSpawnedUnit(CircleObject unit)
        {
            if (LastUnit != null)
                spawnedUnits.Add(LastUnit);
            
            LastUnit = unit;
        }

        public void ClearLastUnit()
        {
            LastUnit = null;
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
        
        public void RemoveUnit(CircleObject unit)
        {
            Score += unit.Score;
            spawnedUnits.Remove(unit);
        }

        public void PushLastUnit()
        {
            if (LastUnit == null)
                return;
            
            spawnedUnits.Add(LastUnit);
            ClearLastUnit();
        }

        public void AddParticleToDisposable(IDisposable particleDisposable)
        {
            particleDisposables.Add(particleDisposable);
        }

        public void ClearParticleDisposables()
        {
            foreach (var disposable in particleDisposables)
                disposable.Dispose();
            
            particleDisposables.Clear();
        }
    }
}