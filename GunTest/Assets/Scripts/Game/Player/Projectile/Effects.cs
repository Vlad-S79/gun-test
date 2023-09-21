using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game.Player
{
    public class Effects
    {
        private ParticleSystem _ref;
        private ObjectPool<ParticleSystem> _pool;

        public void Init(DiContainer container)
        {
            var playerReference = container.GetComponent<PlayerReference>();
            _ref = playerReference.smokeParticleSystem;

            _pool = new ObjectPool<ParticleSystem>(GetEffect);
        }

        private ParticleSystem GetEffect()
        {
            var particleSystem = Object.Instantiate(_ref);
            return particleSystem;
        }

        public void SetEffect(Vector3 position, Vector3 dir, float scale)
        {
            var particleSystem = _pool.GetObject();
            var transform = particleSystem.transform;
            
            transform.position = position;
            transform.forward = dir;
            transform.localScale = Vector3.one * scale;
            
            particleSystem.gameObject.SetActive(true);
            ReturnBackAsync(particleSystem);
        }
        
        private async void ReturnBackAsync(ParticleSystem particleSystem)
        {
            await Task.Delay(2000);
            particleSystem.gameObject.SetActive(false);
            _pool.ReturnObject(particleSystem);
        }
        
        
    }
}