using UnityEngine;
using Deft;

namespace Deft.UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        public delegate void VisibilityChangedHandler(bool visible);
        public VisibilityChangedHandler eventVisibilityChanged;

        public System.Action actionOnClose;

        [System.NonSerialized]
        public Canvas canvas;

        private void Awake()
        {
            eventVisibilityChanged += OnVisibilityChanged;
            OnAwake();
        }

        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    canvas.gameObject.SetActive(value);
                    eventVisibilityChanged?.Invoke(value);
                    actionOnClose?.Invoke();
                    actionOnClose = null;
                }
            }
        }

        public void Show(System.Action onClose = null)
        {
            IsVisible = true;
            actionOnClose = onClose;
        }

        public T Find<T>(string name) where T : Object
            => transform.Find<T>(name);

        protected virtual void OnAwake() { }
        protected virtual void OnVisibilityChanged(bool value) { }
    }
}
