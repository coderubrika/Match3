using UnityEngine;
using UnityEngine.Events;

namespace Test3
{
    public class BaseScreen : MonoBehaviour
    {
        public UnityEvent OnHide { get; } = new();
        
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            OnHide.Invoke();
        }

        public virtual void Init() { }
        
        public virtual void Clear()
        {
            OnHide.RemoveAllListeners();
        }
    }
}