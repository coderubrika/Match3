using Test3.PlayStates;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Test3
{
    public class PlayScreen : BaseScreen, IPointerDownHandler
    {
        [SerializeField] private Color cameraColor;
        
        private Camera mainCamera;
        private ScreenService screenService;
        private PlayService playService;
        
        public override void Init()
        {
            screenService = ServiceLocator.Instance.Get<ScreenService>();
            mainCamera = ServiceLocator.Instance.Get<Camera>();
            playService = ServiceLocator.Instance.Get<PlayService>();
        }

        public override void Show()
        {
            mainCamera.backgroundColor = cameraColor;
            base.Show();
            playService.Start();
            playService.OnFinish += GoToResult;
        }

        private void GoToResult()
        {
            screenService.GoTo<FinishScreen>();
        }
        
        public override void Hide()
        {
            playService.OnFinish -= GoToResult;
            base.Hide();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            playService.Touch();
        }
    }
}