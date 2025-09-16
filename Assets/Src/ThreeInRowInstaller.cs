using UnityEngine;

namespace Test3
{
    public class ThreeInRowInstaller : Installer
    {
        [SerializeField] private UpdateSource updateSource;
        [SerializeField] private CircleObject circleObjectPrefab;

        private Transform circlePoolRoot;
        
        public override void Install()
        {
            Setup();
            
            ServiceLocator.Instance.BindInstance(new ScreensFactory("Screens", "UIRoot"));
            ServiceLocator.Instance.BindInstance(new ScreenService());
            ServiceLocator.Instance.BindInstance(Camera.main);
            ServiceLocator.Instance.BindInstance(updateSource);
            ServiceLocator.Instance.BindInstance(new MonoPool<CircleObject>(circleObjectPrefab, circlePoolRoot));
        }

        private void Setup()
        {
            circlePoolRoot = new GameObject("CirclesPoolRoot").transform;
            DontDestroyOnLoad(circlePoolRoot.gameObject);
        }
    }
}