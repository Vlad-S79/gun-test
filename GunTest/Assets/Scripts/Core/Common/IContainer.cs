using System;

namespace Core
{
    public interface IContainer<TComponent>
    {
        public T GetComponent<T>() where T : TComponent;
        public void AddComponent<T>(T component) where T : TComponent;
        public void AddComponent<T>(Type type, T component) where T : TComponent;
    }
}