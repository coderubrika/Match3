using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Test3
{
    public class ScreensFactory
    {
        private readonly Dictionary<Type, BaseScreen> screens = new();
        private readonly string screensResourcePath;
        
        private readonly Transform screensRoot;
        
        public ScreensFactory(string screensResourcePath, string screenRootName)
        {
            this.screensResourcePath = screensResourcePath;
            screensRoot = new GameObject(screenRootName).transform;
            Object.DontDestroyOnLoad(screensRoot.gameObject);
        }

        public TScreen Get<TScreen>()
            where TScreen : BaseScreen
        {
            Type screenType = typeof(TScreen);
            if (screens.TryGetValue(screenType, out BaseScreen screen))
                return screen as TScreen;

            TScreen prefab = Resources.Load<TScreen>($"{screensResourcePath}/{screenType.Name}");
            TScreen newScreen = Object.Instantiate(prefab, screensRoot);
            newScreen.gameObject.SetActive(false);
            newScreen.Init();
            screens.Add(screenType, newScreen);
            
            return newScreen;
        }
    }
}