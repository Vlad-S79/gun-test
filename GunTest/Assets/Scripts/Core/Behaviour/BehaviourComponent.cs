using UnityEngine;

namespace Core
{
    public class BehaviourComponent : IComponent
    {
        private BehaviourMono _behaviourMono;

        public BehaviourComponent(MonoBehaviour monoBehaviour)
        {
            _behaviourMono = monoBehaviour.gameObject.AddComponent<BehaviourMono>();
        }
        
        public void Init(DiContainer container) { }

        public void AddUpdateBehaviour(IUpdateBehaviour updateBehaviour)
            => _behaviourMono.updateBehaviours.Add(updateBehaviour);
        
        public void RemoveUpdateBehaviour(IUpdateBehaviour updateBehaviour)
            => _behaviourMono.updateBehaviours.Remove(updateBehaviour);
        
        
        public void AddFixedUpdateBehaviour(IFixedUpdateBehaviour fixedUpdateBehaviour)
            => _behaviourMono.fixedUpdateBehaviours.Add(fixedUpdateBehaviour);
        
        public void RemoveFixedUpdateBehaviour(IFixedUpdateBehaviour fixedUpdateBehaviour)
            => _behaviourMono.fixedUpdateBehaviours.Remove(fixedUpdateBehaviour);
    }
}