using System.Threading.Tasks;
using Core;
using Game.Camera;
using Game.Input;
using UnityEngine;

namespace Game.Player
{
    public class PlayerComponent : IComponent
    {
        private PlayerBehaviour _playerBehaviour;
        private Transform _gunBaseTransform;

        private Vector3 _position = new Vector3(.3f,0,-4.5f);
        private TrajectoryGun _trajectoryGun;
        private Projectiles _projectiles;

        private PlayerReference _playerReference;

        private float _power;
        private float _scale;
        private float _dumping;

        private float _shakeValue;

        private float _timeAnimationSeconds = .2f;
        private float _currentTimeAnimation;

        private float _animationMagnitude = .1f;
        private AnimationCurve _animGunAnimationCurve;

        public void Init(DiContainer container)
        {
            _playerReference = container.GetComponent<PlayerReference>();
            _playerBehaviour = Object.Instantiate(_playerReference.playerBehaviour);
            
            _gunBaseTransform = _playerBehaviour.transform;
            _gunBaseTransform.position = _position;
            
            _trajectoryGun = new TrajectoryGun(_playerBehaviour.gunTransform);
            _trajectoryGun.Init(container);

            _projectiles = new Projectiles();
            _projectiles.Init(container);

            var inputComponent = container.GetComponent<InputComponent>();
            
            inputComponent.AddKeyAction(new KeyCodeAction(KeyCode.LeftArrow, ()=>RotateGunHorizontal(-1)));
            inputComponent.AddKeyAction(new KeyCodeAction(KeyCode.RightArrow, ()=>RotateGunHorizontal(1)));
            inputComponent.AddKeyAction(new KeyCodeAction(KeyCode.DownArrow, ()=>RotateGunVertical(-1)));
            inputComponent.AddKeyAction(new KeyCodeAction(KeyCode.UpArrow, ()=>RotateGunVertical(1)));
            
            inputComponent.AddKeyDownAction(new KeyCodeAction(KeyCode.Space, CreateProjectile));

            var cameraComponent = container.GetComponent<CameraComponent>();
            var camera = cameraComponent.Camera;
            camera.transform.SetParent(_gunBaseTransform);
            
            _animGunAnimationCurve = AnimationCurve.EaseInOut(0,0,.5f,1);
            _animGunAnimationCurve.postWrapMode = WrapMode.PingPong;
            _currentTimeAnimation = _timeAnimationSeconds;
        }

        private async void GunAnimationAsync()
        {
            _currentTimeAnimation = 0;
            var oldPos = _playerBehaviour.gunMeshTransform.localPosition;
            while (_currentTimeAnimation < _timeAnimationSeconds)
            {
                _currentTimeAnimation += Time.deltaTime;
                var t = _animGunAnimationCurve.Evaluate(_currentTimeAnimation / _timeAnimationSeconds);
                _playerBehaviour.gunMeshTransform.localPosition = oldPos + Vector3.back * _animationMagnitude * t;
                await Task.Yield();
            }

            _currentTimeAnimation = _timeAnimationSeconds;
            _playerBehaviour.gunMeshTransform.localPosition = oldPos;
        }

        private void RotateGunHorizontal(float value)
        {
            _gunBaseTransform.Rotate(Vector3.up, value * _playerReference.rotateHorizontalSpeed * Time.deltaTime);
            _trajectoryGun.Update();
        }

        private void RotateGunVertical(float value)
        {
            _playerBehaviour.gunTransform.Rotate(Vector3.left, value * _playerReference.rotateVerticalSpeed * Time.deltaTime);
            _trajectoryGun.Update();
        }

        private void CreateProjectile()
        {
            if (_currentTimeAnimation < _timeAnimationSeconds)
            {
                var halTimeAnimation = _timeAnimationSeconds / 2;
                if (_currentTimeAnimation > halTimeAnimation)
                {
                    _currentTimeAnimation -= _currentTimeAnimation - halTimeAnimation;
                }
                else
                {
                    _currentTimeAnimation = _timeAnimationSeconds - _currentTimeAnimation;
                }
            }
            else
            {
                GunAnimationAsync();
            }
            
            _projectiles.CreateProjectile(_playerBehaviour.gunTransform.position, 
                _playerBehaviour.gunTransform.forward, _power, _scale, _dumping, _shakeValue);
        }

        public void SetPlayerPower(float value)
        {
            _shakeValue = value;
            _dumping = Mathf.Lerp(_playerReference.minDumpingHit, _playerReference.maxDumpingHit, value);
            _scale = Mathf.Lerp(_playerReference.minProjectileSize, _playerReference.maxProjectileSize, 1 - value);
            _power = Mathf.Lerp(_playerReference.minProjectileSpeed, _playerReference.maxProjectileSpeed, value);
            _trajectoryGun.SetPower(_power);
        }
    }
}