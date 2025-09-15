using UnityEngine;

namespace Test3
{
    public class PlayScreen : BaseScreen
    {
        [SerializeField] private Color cameraColor;
        [SerializeField] private Field fieldPrefab;

        private Field field;
        private Camera mainCamera;
        private ScreenService screenService;
        
        public override void Init()
        {
            screenService = ServiceLocator.Instance.Get<ScreenService>();
            mainCamera = ServiceLocator.Instance.Get<Camera>();
            field = Instantiate(fieldPrefab);
            field.gameObject.SetActive(false);
            DontDestroyOnLoad(field.gameObject);
        }

        public override void Show()
        {
            mainCamera.backgroundColor = cameraColor;
            base.Show();
            
            field.Init();
            field.gameObject.SetActive(true);
        }

        public override void Hide()
        {
            field.gameObject.SetActive(false);
            base.Hide();
        }
    }
}