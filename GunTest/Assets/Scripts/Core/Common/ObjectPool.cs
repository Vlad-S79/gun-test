using System;
using System.Collections.Generic;

namespace Core
{
    public class ObjectPool<T>
    {
        private Queue<T> _container;
        private Action<T> _onInitAction;
        private Func<T> _getInstanceFunc;

        public ObjectPool(Func<T> getInstanceFunc, Action<T> onInitAction = null)
        {
            _container = new Queue<T>();
            _onInitAction = onInitAction;
            _getInstanceFunc = getInstanceFunc;
        }

        public T GetObject()
        {
            T tObject;
            
            if(_container.Count == 0)
            {
                tObject = _getInstanceFunc();
                _onInitAction?.Invoke(tObject);
            }
            else
            {
                tObject = _container.Dequeue();
            }

            return tObject;
        }

        public void ReturnObject(T tObject)
        {
            _container.Enqueue(tObject);
        }

        public void Clear(Action<T> onClearAction = null)
        {
            if (onClearAction == null)
            {
                _container.Clear();
                return;    
            }
            
            while(_container.Count > 0)
            {
                var tObject = _container.Dequeue();
                onClearAction.Invoke(tObject);
            }
            
            _container.Clear();
        }
    }
}