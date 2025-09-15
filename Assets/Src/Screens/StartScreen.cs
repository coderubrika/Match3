using UnityEngine;
using UnityEngine.UI;

namespace Test3
{
    public class StartScreen : BaseScreen
    {
        [SerializeField] private Button playButton;
        [SerializeField] private GameObject playAnimationPrefab;
        [SerializeField] private Color cameraColor;

        private Camera mainCamera;
        private ScreenService screenService;
        private GameObject playAnimationObject;
        
        public override void Init()
        {
            playAnimationObject = Instantiate(playAnimationPrefab);
            playAnimationObject.gameObject.SetActive(false);
            DontDestroyOnLoad(playAnimationObject);
            
            screenService = ServiceLocator.Instance.Get<ScreenService>();
            mainCamera = ServiceLocator.Instance.Get<Camera>();
        }

        public override void Show()
        {
            mainCamera.backgroundColor = cameraColor;
            playButton.onClick.AddListener(() => screenService.GoTo<PlayScreen>());
            base.Show();
            
            playAnimationObject.SetActive(true);
        }

        public override void Hide()
        {
            playAnimationObject.gameObject.SetActive(false);
            base.Hide();
        }

        public override void Clear()
        {
            base.Clear();
            playButton.onClick.RemoveAllListeners();
        }
    }
}