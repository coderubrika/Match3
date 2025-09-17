using Test3.PlayStates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test3
{
    public class FinishScreen : BaseScreen
    {
        [SerializeField] private TMP_Text score;
        [SerializeField] private Button playAgain;
        [SerializeField] private Button toMainScreen;
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
            score.text = playService.Score.ToString();
            mainCamera.backgroundColor = cameraColor;
            playAgain.onClick.AddListener(() => screenService.GoTo<PlayScreen>());
            toMainScreen.onClick.AddListener(() => screenService.GoTo<StartScreen>());
            
            base.Show();
        }
        
        public override void Clear()
        {
            base.Clear();
            playAgain.onClick.RemoveAllListeners();
            toMainScreen.onClick.RemoveAllListeners();
        }
    }
}