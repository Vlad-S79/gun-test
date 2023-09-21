using Core;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "storage_reference", menuName = "Game/Storage Reference", order = 1)]
    public class Storage : ScriptableObject, IComponent
    {
        [SerializeField] private Reference[] _referencesContainer;
        
        public void Init(DiContainer container)
        {
            foreach (var reference in _referencesContainer)
            {
                var type = reference.GetType();
                reference.Init(container);
                
                container.AddComponent(type, reference);
            }
        }
    }
}