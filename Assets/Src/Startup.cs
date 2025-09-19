using UnityEngine;

namespace Test3
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Installer installer;
        
        private void Awake()
        {
            installer.Install();
            Application.targetFrameRate = 120;
            ServiceLocator.Instance.Get<ScreenService>().GoTo<StartScreen>();
        }
    }
}