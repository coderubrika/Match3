using UnityEngine;

namespace Test3
{
    public class ThreeInRowInstaller : Installer
    {
        [SerializeField] private UpdateSource updateSource;
        
        public override void Install()
        {
            ServiceLocator.Instance.BindInstance(new ScreensFactory("Screens", "UIRoot"));
            ServiceLocator.Instance.BindInstance(new ScreenService());
            ServiceLocator.Instance.BindInstance(Camera.main);
            ServiceLocator.Instance.BindInstance(updateSource);
        }
    }
}