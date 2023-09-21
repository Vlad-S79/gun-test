using System.Linq;
using UnityEngine;

namespace Core.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private WindowType _windowType;
        
        protected UiComponent UiComponent;
        
        public void Init(DiContainer container)
        {
            UiComponent = container.GetComponent<UiComponent>();
            
            var initChildren = transform.GetComponentsInChildren<IInit>(true);
            foreach (var initChild in initChildren) 
            {
                initChild.Init(container);
            }

            OnInit(container);
        }
        
        public void Open()
        {
            var siblingIndex = UiComponent.GetSiblingIndex(WindowType);
            transform.SetSiblingIndex(siblingIndex);
            
            gameObject.SetActive(true);
            OnOpen();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            OnClose();
        }

        protected abstract void OnInit(DiContainer container);
        
        protected abstract void OnOpen();
        protected abstract void OnClose();

        public bool IsOpen() => gameObject.activeSelf;

        public WindowType WindowType => _windowType;
    }
}