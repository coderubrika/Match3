using Test3.PlayStates;
using UnityEngine;

namespace Test3
{
    public class ThreeInRowInstaller : Installer
    {
        [SerializeField] private UpdateSource updateSource;
        [SerializeField] private CircleObject circleObjectPrefab;
        [SerializeField] private Pendulum pendulumPrefab;
        [SerializeField] private Field fieldPrefab;
        [SerializeField] private GameConfig gameConfig;
        
        private Transform circlePoolRoot;
        
        public override void Install()
        {
            Setup();
            
            ServiceLocator.Instance.BindInstance(gameConfig);
            ServiceLocator.Instance.BindInstance(new ScreensFactory("Screens", "UIRoot"));
            ServiceLocator.Instance.BindInstance(new ScreenService());
            ServiceLocator.Instance.BindInstance(Camera.main);
            ServiceLocator.Instance.BindInstance(updateSource);
            ServiceLocator.Instance.BindInstance(new MonoPool<CircleObject>(circleObjectPrefab, circlePoolRoot));
            ServiceLocator.Instance.BindInstance(fieldPrefab);
            ServiceLocator.Instance.BindInstance(pendulumPrefab);
            ServiceLocator.Instance.BindInstance(new PlayStatesFactory());
            ServiceLocator.Instance.BindInstance(new PlayService());
        }

        private void Setup()
        {
            circlePoolRoot = new GameObject("CirclesPoolRoot").transform;
            DontDestroyOnLoad(circlePoolRoot.gameObject);
        }
    }
}