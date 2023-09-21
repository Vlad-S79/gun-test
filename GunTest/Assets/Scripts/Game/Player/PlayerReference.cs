using Core;
using Game.Data;
using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(fileName = "player_reference", menuName = "Game/Player Reference", order = 1)]
    public class PlayerReference : Reference
    {
        [Space]
        public PlayerBehaviour playerBehaviour;
        public ParticleSystem smokeParticleSystem;
        public GameObject mark;
        [Space]
        public Material trajectoryLineMaterial;
        public Material projectileMaterial;
        [Space]
        [Range(0, 100)] public int defaultPower;
        public float rotateHorizontalSpeed;
        public float rotateVerticalSpeed;
        [Space]
        public float minHorizontalAngel;
        public float maxHorizontalAngel;
        public float minVerticalAngel;
        public float maxVerticalAngel;
        [Space] 
        public float minProjectileSpeed;
        public float maxProjectileSpeed;
        [Space]
        public float minProjectileSize;
        public float maxProjectileSize;
        [Space] 
        public float maxProjectileLifeTimeSeconds;
        [Space] 
        public float gravity;
        public int trajectoryTimeSeconds;
        [Range(0, 1)] public float minDumpingHit;
        [Range(0, 1)] public float maxDumpingHit;
        

        public override void Init(DiContainer container) { }
    }
}