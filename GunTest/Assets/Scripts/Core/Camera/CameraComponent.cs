using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game.Camera
{
    public class CameraComponent : IComponent, IUpdateBehaviour
    {
        private CameraData _cameraData;
        private ICameraContainer _cameraContainer;

        private HashSet<ICameraContainer> _cameraContainerEffect;
        
        private UnityEngine.Camera _camera;
        private Transform _cameraTransform;
        
        private Action _onCompleteEffects;

        public UnityEngine.Camera Camera => _camera;

        public void Init(DiContainer container)
        {
            _cameraContainerEffect = new HashSet<ICameraContainer>();
            _cameraData = new CameraData();
            
            var behaviourComponent = container.GetComponent<BehaviourComponent>();
            behaviourComponent.AddUpdateBehaviour(this);

            var cameraGameObject = new GameObject("camera");
            _camera = cameraGameObject.AddComponent<UnityEngine.Camera>();

            _cameraTransform = _camera.transform;
        }

        public void Update()
        {
            _cameraContainer?.Update();

            foreach (var cameraContainerEffect in _cameraContainerEffect)
            {
                cameraContainerEffect.Update();
            }

            _onCompleteEffects?.Invoke();
            _onCompleteEffects = null;
            
            UpdateCameraData();
        }

        public void SetCameraContainer(ICameraContainer cameraContainer)
        {
            cameraContainer.SetCameraData(_cameraData);
            _cameraContainer = cameraContainer;
        }

        public void AddCameraEffect(ICameraContainer cameraContainerEffect)
        {
            if (!_cameraContainerEffect.Contains(cameraContainerEffect))
            {
                cameraContainerEffect.SetCameraData(_cameraData);
                _cameraContainerEffect.Add(cameraContainerEffect);
            }
        }

        public void RemoveCameraEffect(ICameraContainer cameraContainerEffect)
        {
            if(_cameraContainerEffect.Contains(cameraContainerEffect))
                _onCompleteEffects += () 
                    => _cameraContainerEffect.Remove(cameraContainerEffect);
        }

        private void UpdateCameraData()
        {
            _cameraTransform.localPosition = _cameraData.Position;
            _cameraTransform.localRotation = _cameraData.Rotation;
            _camera.fieldOfView = _cameraData.FieldOfView;
        }
    }
}