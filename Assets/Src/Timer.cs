using System;
using UnityEngine;

namespace Test3
{
    public class Timer : IDisposable
    {
        private readonly float time;
        private readonly UpdateSource updateSource;
        private readonly Action callback;
        
        private float currentTime;
        private bool isDisposed;
        
        public Timer(UpdateSource updateSource, Action callback, float time)
        {
            this.time = time;
            this.callback = callback;
            this.updateSource = updateSource;
            
            updateSource.OnUpdate += Update;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;

            if (currentTime < time) 
                return;
            
            callback?.Invoke();
            Dispose();
        }
        
        public void Dispose()
        {
            if (isDisposed)
                return;
            
            isDisposed = true;
            updateSource.OnUpdate -= Update;
        }
    }
}