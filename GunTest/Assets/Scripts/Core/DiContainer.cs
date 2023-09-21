using System;

namespace Core
{
    // third-party assets did you mean zenject too?

    public class DiContainer : IContainer<IComponent>
    {
        private Container<IComponent> _container;

        public DiContainer()
        {
            _container = new Container<IComponent>();
            _container.OnAddComponent += OnAddComponent;
        }
        private void OnAddComponent(IComponent component)
        {
            component.Init(this);
        }

        public T GetComponent<T>() where T : IComponent
            => _container.GetComponent<T>();

        public void AddComponent<T>(T component) where T : IComponent
            => _container.AddComponent(component);

        public void AddComponent<T>(Type type, T component) where T : IComponent
            => _container.AddComponent(type, component);
    }
}