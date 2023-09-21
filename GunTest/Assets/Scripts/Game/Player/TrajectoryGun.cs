using Core;
using UnityEngine;

namespace Game.Player
{
    public class TrajectoryGun
    {
        private LineRenderer _lineRenderer;
        private Vector3[] _pointsArray;
        private int _maxPoints;

        private PlayerReference _playerReference;

        private float _power;
        private Transform _emitter;

        private float _timeStep;

        public TrajectoryGun(Transform emitter, int maxPoints = 50)
        {
            _emitter = emitter;
            _maxPoints = maxPoints;
            _pointsArray = new Vector3[maxPoints];

            _lineRenderer = emitter.gameObject.AddComponent<LineRenderer>();
            _lineRenderer.endWidth = 0; 
            _lineRenderer.startWidth = 0.03f;
        }

        public void Init(DiContainer container)
        {
            _playerReference = container.GetComponent<PlayerReference>();
            _lineRenderer.material = _playerReference.trajectoryLineMaterial;
            _lineRenderer.loop = false;
            
            _timeStep = (float) _playerReference.trajectoryTimeSeconds / _maxPoints;
        }

        public void Update()
        {
            UpdateTrajectory();

            _lineRenderer.positionCount = _maxPoints;
            _lineRenderer.SetPositions(_pointsArray);
        }

        private void UpdateTrajectory()
        {
            for (var i = 0; i < _maxPoints; i++)
            {
                var t = i * _timeStep;
                _pointsArray[i] = _emitter.position + _power * _emitter.forward * t;
                _pointsArray[i].y -= 0.5f * _playerReference.gravity * t * t;
            }
        }

        public void SetPower(float power)
        {
            _power = power;
            Update();
        }
    }
}