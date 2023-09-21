using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.UI
{
    public class UiComponent : IComponent
    {
        private Container<Window> _container;
        private int[] _siblingIndexes;

        private DiContainer _diContainer;
        private UiReference _uiReference;

        private Transform _canvasTransform;

        public UiComponent()
        {
            _container = new Container<Window>();
            
            var windowTypeCount = Enum.GetNames(typeof(WindowType)).Length;
            _siblingIndexes = new int[windowTypeCount];
        }
        
        public void Init(DiContainer container)
        {
            _diContainer = container;
            _uiReference = container.GetComponent<UiReference>();

            var canvas = Object.Instantiate(_uiReference.canvas);
            var evenSystem = Object.Instantiate(_uiReference.eventSystem);

            _canvasTransform = canvas.transform;
        }

        public int GetSiblingIndex(WindowType windowType)
        {
            var windowTypeIndex = (int) windowType;
            var index = 0;

            for (var i = 0; i < windowTypeIndex; i++)
            {
                index += _siblingIndexes[i];
            }

            return index + 1;
        }

        public T GetWindow<T>() where T : Window
        {
            if (_container.ContainsKey<T>())
            {
                return _container.GetComponent<T>();
            }

            return InitWindow<T>();
        }

        private T InitWindow<T>() where T : Window
        {
            var reference = _uiReference.GetWindow<T>();
            reference.gameObject.SetActive(false);

            var window = Object.Instantiate(reference, _canvasTransform);
            reference.gameObject.SetActive(true);

            var type = typeof(T);
            _container.AddComponent(type, window);
            window.Init(_diContainer);

            var windowTypeIndex = (int) window.WindowType;
            _siblingIndexes[windowTypeIndex]++;
            
            return (T) window;
        }
    }
}