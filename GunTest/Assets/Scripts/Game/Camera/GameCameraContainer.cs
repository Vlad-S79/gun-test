using UnityEngine;

namespace Game.Camera
{
    public class GameCameraContainer : ICameraContainer
    {
        private CameraData _cameraData;
        private Vector3 _offset = new Vector3(-.3f, .4f, -.5f);
        
        public void SetCameraData(CameraData cameraData)
        {
            _cameraData = cameraData;
        }

        public void Update()
        {
            _cameraData.Position = _offset;
        }
    }
}