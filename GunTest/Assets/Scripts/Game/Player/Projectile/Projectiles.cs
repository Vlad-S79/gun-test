using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game.Player
{
    public class Projectiles : IFixedUpdateBehaviour
    {
        private ProjectileMeshGenerator _projectileMeshGenerator;
        private ObjectPool<Projectile> _projectilePool;

        private List<Projectile> _projectilesInScene;

        private Action _afterCompleteUpdate;
        public Marks Marks;
        public Effects Effects;

        public void Init(DiContainer container)
        {
            Marks = new Marks();
            Marks.Init(container);

            Effects = new Effects();
            Effects.Init(container);
            
            _projectilesInScene = new List<Projectile>();
            _projectilePool = new ObjectPool<Projectile>(() => new Projectile(),
                projectile => projectile.Init(container, this));
            
            _projectileMeshGenerator = new ProjectileMeshGenerator();
            
            var behaviourComponent = container.GetComponent<BehaviourComponent>();
            behaviourComponent.AddFixedUpdateBehaviour(this);
        }

        public void CreateProjectile(Vector3 emitter, Vector3 direction, 
            float power, float scale, float dumping, float shakeValue)
        {
            var projectile = _projectilePool.GetObject();
            _projectileMeshGenerator.UpdateMesh(projectile.Mesh, scale, scale * .1f);
            projectile.SetMovement(emitter, direction, power, scale, dumping, shakeValue);
            projectile.SetActive(true);
            
            _projectilesInScene.Add(projectile);
        }

        public void ReturnProjectile(Projectile projectile)
        {
            _afterCompleteUpdate += () =>
            {
                _projectilePool.ReturnObject(projectile);
                _projectilesInScene.Remove(projectile);
                projectile.SetActive(false);
            };
        }

        public void FixedUpdate()
        {
            foreach (var projectile in _projectilesInScene)
            {
                projectile.FixedUpdate();
            }
            
            _afterCompleteUpdate?.Invoke();
            _afterCompleteUpdate = null;
        }
    }
}