using Core;
using Game.Camera;
using UnityEngine;

namespace Game.Player
{
    public class Projectile
    {
        private static int _index;
        private GameObject _gameObject;
        private Vector3 _movement;
        private Vector3 _position;
        private Vector3 _oldPosition;
        private Vector3 _direction;
        private float _dumping;

        private float _scale;
        public Mesh Mesh;

        private PlayerReference _playerReference;

        private RaycastHit _raycastHit;

        private float _shakeValue;
        private float _lifeTime;
        private int _reflectionCounter;
        private Transform _transform;
        
        private Projectiles _projectiles;
        private GameCameraComponent _gameCameraComponent;
        
        public void Init(DiContainer container, Projectiles projectiles)
        {
            _projectiles = projectiles;
            _playerReference = container.GetComponent<PlayerReference>();
            _gameCameraComponent = container.GetComponent<GameCameraComponent>();
            
            _gameObject = new GameObject("projectile_" + _index);
            _transform = _gameObject.transform;
            _index++;
            
            var meshRenderer = _gameObject.AddComponent<MeshRenderer>();
            meshRenderer.material = _playerReference.projectileMaterial;
            
            var meshFilter = _gameObject.AddComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            Mesh = meshFilter.mesh;
            
            
        }

        public void SetMovement(Vector3 position, Vector3 direction, float power, 
            float scale, float dumping, float shakeValue)
        {
            _shakeValue = shakeValue;
            
            _position = position;
            _movement = direction * power;
            
            _scale = scale;

            _reflectionCounter = 0;
            _lifeTime = 0;
            
            _transform.forward = new Vector3(direction.x, 0, direction.z);
            _dumping = dumping;
            
            _transform.position = _position;
            
            _gameCameraComponent.CustomShake(.002f);
        }

        public void FixedUpdate()
        {
            _lifeTime += Time.fixedDeltaTime;
            if (_lifeTime >= _playerReference.maxProjectileLifeTimeSeconds)
            {
                ReturnObject();
                _projectiles.Effects.SetEffect(_position, Vector3.up, _scale);
                return;
            }
            
            _oldPosition = _position;
            _position += _movement * Time.fixedDeltaTime;
            _movement.y -= _playerReference.gravity * Time.fixedDeltaTime;
            
            var dir = _movement.normalized;
            var maxDistance = _movement.magnitude * Time.fixedDeltaTime;

            if (Physics.BoxCast(_oldPosition, _scale / 2 * Vector3.one, dir, out _raycastHit,
                _transform.rotation, maxDistance))
            {
                _position = _oldPosition;
                _movement = Vector3.Reflect(_movement * _dumping, _raycastHit.normal);

                _reflectionCounter++;

                if (_reflectionCounter >= 2)
                {
                    ReturnObject();
                    _projectiles.Effects.SetEffect(_position, _raycastHit.normal, _scale);
                }

                if (_raycastHit.transform.CompareTag("wall"))
                {
                    _projectiles.Marks.SetMark(_raycastHit);
                }
            }

            _transform.position = _position;
        }

        private void ReturnObject()
        {
            _projectiles.ReturnProjectile(this);
            _gameCameraComponent.Shake(1 - _shakeValue);
        }

        public void SetActive(bool value)
        {
            _gameObject.SetActive(value);
        }
    }
}