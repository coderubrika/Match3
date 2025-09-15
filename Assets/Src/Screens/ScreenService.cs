namespace Test3
{
    public class ScreenService
    {
        private readonly ScreensFactory screensFactory;
        private BaseScreen currentScreen;

        public ScreenService()
        {
            screensFactory = ServiceLocator.Instance.Get<ScreensFactory>();
        }
        
        public void GoTo<TScreen>()
            where TScreen : BaseScreen
        {
            if (currentScreen != null && typeof(TScreen) == currentScreen.GetType())
                return;

            BaseScreen oldScreen = currentScreen;
            currentScreen = screensFactory.Get<TScreen>();
            SetupScreen(oldScreen, currentScreen);
        }

        private void SetupScreen(BaseScreen prev, BaseScreen next)
        {
            if (prev != null)
            {
                prev.OnHide.AddListener(() =>
                {
                    prev.Clear();
                    next.Show();
                });
                
                prev.Hide();
                
                return;
            }
            
            next.Show();
        }
    }
}