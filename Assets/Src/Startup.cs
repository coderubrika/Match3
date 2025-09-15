using UnityEngine;

namespace Test3
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Installer installer;
        
        private void Awake()
        {
            installer.Install();
            ServiceLocator.Instance.Get<ScreenService>().GoTo<StartScreen>();
        }
    }
}