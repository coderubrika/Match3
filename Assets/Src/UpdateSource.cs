using System;
using UnityEngine;
using UnityEngine.Events;

namespace Test3
{
    public class UpdateSource : MonoBehaviour
    {
        public event Action OnUpdate;
        
        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}