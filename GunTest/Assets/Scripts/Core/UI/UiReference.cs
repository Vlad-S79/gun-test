using Game.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI
{
    [CreateAssetMenu(fileName = "ui_reference", menuName = "Game/UI Reference", order = 1)]
    public class UiReference : Reference
    {
        public Canvas canvas;
        public EventSystem eventSystem;
            
        [SerializeField] private Window[] _windows;
        
        private Container<Window> _container;

        public override void Init(DiContainer container)
        {
            _container = new Container<Window>();
            
            foreach (var window in _windows)
            {
                var type = window.GetType();
                _container.AddComponent(type, window);
            }
        }

        public Window GetWindow<T>() where T : Window
            => _container.GetComponent<T>();
    }
}