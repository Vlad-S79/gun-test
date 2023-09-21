using Core;
using Game.Camera.Common;
using UnityEngine;

namespace Game.Camera
{
    public class GameCameraComponent : IComponent
    {
        private CameraComponent _cameraComponent;
        
        private ShakeCameraContainerEffect _shakeCameraContainerEffect;

        private float _minShakeMagnitude;
        private float _maxShakeMagnitude;
        
        public void Init(DiContainer container)
        {
            _cameraComponent = container.GetComponent<CameraComponent>();
            _cameraComponent.SetCameraContainer(new GameCameraContainer());
            
            _minShakeMagnitude = .005f;
            _maxShakeMagnitude = .02f;
            
            _shakeCameraContainerEffect = new ShakeCameraContainerEffect(_cameraComponent);
        }
        

        // Range from 0 to 1
        public void Shake(float value)
        {
            var magnitude = Mathf.Lerp(_minShakeMagnitude, _maxShakeMagnitude, value);
            CustomShake(magnitude);
        }

        public void CustomShake(float magnitude)
        {
            _shakeCameraContainerEffect.SetData(.1f, magnitude);
            _cameraComponent.AddCameraEffect(_shakeCameraContainerEffect);
        }
    }
}