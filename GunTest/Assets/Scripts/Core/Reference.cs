using Core;
using UnityEngine;

namespace Game.Data
{
    public abstract class Reference : ScriptableObject , IComponent
    {
        public abstract void Init(DiContainer container);
    }
}