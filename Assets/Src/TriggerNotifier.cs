using System;
using UnityEngine;

namespace Test3
{
    public class TriggerNotifier : MonoBehaviour
    {
        public event Action<Collider2D> OnTriggerEnter;
        public event Action<Collider2D> OnTriggerExit;
        
        private void OnTriggerEnter2D(Collider2D other) => OnTriggerEnter?.Invoke(other);
        private void OnTriggerExit2D(Collider2D other) => OnTriggerExit?.Invoke(other);

        public void ClearAllSubscriptions()
        {
            OnTriggerEnter = null;
            OnTriggerExit = null;
        }
    }
}