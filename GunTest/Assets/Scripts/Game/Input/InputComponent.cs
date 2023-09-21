using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game.Input
{
    public class InputComponent : IComponent, IUpdateBehaviour
    {
        private List<KeyCodeAction> _keyPressContainer;
        private List<KeyCodeAction> _keyDownContainer;

        public void Init(DiContainer container)
        {
            _keyPressContainer = new List<KeyCodeAction>();
            _keyDownContainer = new List<KeyCodeAction>();

            var behaviourComponent = container.GetComponent<BehaviourComponent>();
            behaviourComponent.AddUpdateBehaviour(this);
        }

        public void Update()
        {
            foreach (var keyCodeAction in _keyPressContainer)
            {
                if (UnityEngine.Input.GetKey(keyCodeAction.KeyCode))
                {
                    keyCodeAction.Action.Invoke();
                }
            }
            
            foreach (var keyCodeAction in _keyDownContainer)
            {
                if (UnityEngine.Input.GetKeyUp(keyCodeAction.KeyCode))
                {
                    keyCodeAction.Action.Invoke();
                }
            }
        }

        public void AddKeyAction(KeyCodeAction keyCodeAction)
            => _keyPressContainer.Add(keyCodeAction);

        public void RemoveKeyAction(KeyCodeAction keyCodeAction)
            => _keyPressContainer.Remove(keyCodeAction);
        
        public void AddKeyDownAction(KeyCodeAction keyCodeAction)
            => _keyDownContainer.Add(keyCodeAction);
        
        public void RemoveDownAction(KeyCodeAction keyCodeAction)
            => _keyDownContainer.Remove(keyCodeAction);
    }
}