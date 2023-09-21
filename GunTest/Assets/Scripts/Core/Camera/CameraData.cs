using UnityEngine;

namespace Game.Camera
{
    public class CameraData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public float FieldOfView;

        public CameraData()
        {
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
            FieldOfView = 60;
        }
    }
}