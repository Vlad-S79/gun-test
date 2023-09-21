using Core;
using Game.Data;
using UnityEngine;

namespace Game.Level
{
    [CreateAssetMenu(fileName = "level_reference", menuName = "Game/Level Reference", order = 1)]
    public class LevelReference : Reference
    {
        public GameObject levelPrefab;
        
        public override void Init(DiContainer container) { }
    }
}