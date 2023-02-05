using System;
using BountyHunter.Utils;
using UnityEngine;

namespace BountyHunter
{
    public sealed class CameraMover : IDisposable
    {
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly Camera _camera;
        private readonly Settings _settings;

        public CameraMover(TickInvoker tickInvoker, PlayerHolder playerHolder, Camera camera, Settings settings)
        {
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
            _camera = camera;
            _settings = settings;
        }

        public void Activate()
        {
            _tickInvoker.LateUpdated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.LateUpdated -= OnUpdated;
        }

        public void Dispose()
        {
            Deactivate();
        }

        private void OnUpdated()
        {
            if (!_playerHolder.HasPlayerShip)
                return;

            Ship ship = _playerHolder.PlayerShip;
            float shipAngle = ship.Angle;
            float radianAngle = Mathf.Deg2Rad * shipAngle;
            float sin = Mathf.Sin(radianAngle);
            float cos = Mathf.Cos(radianAngle);

            float cameraOffset = _settings.CameraDistanceOffset;
            Transform cameraTransform = _camera.transform;
            Vector3 cameraPosition = cameraTransform.position;

            Vector2 shipPosition = ship.Position;
            cameraPosition.x = shipPosition.x + cos * cameraOffset;
            cameraPosition.y = shipPosition.y + sin * cameraOffset;

            cameraTransform.position = cameraPosition;
            float angle = ship.Angle + _settings.CameraAngleOffset;
            cameraTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}