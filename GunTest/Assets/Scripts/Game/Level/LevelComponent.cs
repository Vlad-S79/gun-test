using Core;
using UnityEngine;

namespace Game.Level
{
    public class LevelComponent : IComponent
    {
        public void Init(DiContainer container)
        {
            var levelReference = container.GetComponent<LevelReference>();

            Object.Instantiate(levelReference.levelPrefab);
        }
    }
}