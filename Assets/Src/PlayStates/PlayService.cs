using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Test3.PlayStates
{
    public class PlayService
    {
        private readonly Transform gameRoot;
        private readonly Field field;
        private readonly Pendulum pendulum;

        private PlaySession session;
        private PlayContext context;
        
        public event Action OnFinish
        {
            add => context.OnFinish += value;
            remove => context.OnFinish -= value;
        }
        
        public PlayService()
        {
            gameRoot = new GameObject("GAME_ROOT").transform;
            Object.DontDestroyOnLoad(gameRoot.gameObject);
            
            Pendulum pendulumPrefab = ServiceLocator.Instance.Get<Pendulum>();
            pendulum = Object.Instantiate(pendulumPrefab, gameRoot);
            pendulum.gameObject.SetActive(false);
            
            Field fieldPrefab = ServiceLocator.Instance.Get<Field>();
            field = Object.Instantiate(fieldPrefab, gameRoot);
            field.gameObject.SetActive(false);
        }

        public void Start()
        {
            context = new PlayContext(field, pendulum, gameRoot);
            session = new PlaySession(context);
        }

        public void Touch()
        {
            session?.Touch();
        }
    }
}