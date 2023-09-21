using System;
using System.Collections.Generic;

namespace Core
{
    public class Container<TComponent> : IContainer<TComponent>
    {
        private Dictionary<Type, TComponent> _container;
        public event Action<TComponent> OnAddComponent;

        public Container()
        {
            _container = new Dictionary<Type, TComponent>();
        }

        public T GetComponent<T>() where T : TComponent
        {
            var type = typeof(T);
            if(_container.TryGetValue(type, out var result))
            {
                return (T)result;
            }

            throw new Exception("DiContainer Not Have " + type + "Component");
        }

        public void AddComponent<T>(T component) where T : TComponent
        {
            var type = typeof(T);
            AddComponent(type, component);
        }

        public void AddComponent<T>(Type type, T component) where T : TComponent
        {
            OnAddComponent?.Invoke(component);
            _container[type] = component;
        }

        public bool ContainsKey<T>()
        {
            var type = typeof(T);
            return _container.ContainsKey(type);
        }
    }
}