using UnityEngine;
using Object = UnityEngine.Object;

namespace BountyHunter
{
    public class Ship
    {
        private readonly ShipHierarchy _view;
        private readonly IShipSettings _shipSettings;

        public IShipInput ShipInput { get; }
        public Vector2 Position { get; private set; }
        public float Angle { get; private set; }
        public float ForwardSpeed { get; private set; }
        public float RotateSpeed { get; private set; }
        public float StrafeSpeed { get; private set; }

        public Ship(ShipHierarchy view, IShipInput shipInput, IShipSettings shipSettings)
        {
            ShipInput = shipInput;
            _view = view;
            _shipSettings = shipSettings;

            UpdateView();
        }

        public void SetPosition(Vector2 position, float angle)
        {
            Position = position;
            Angle = angle;
        }

        public void UpdateMove(float deltaTime)
        {
            UpdateSpeeds(deltaTime);
            UpdatePosition(deltaTime);
            UpdateView();
        }

        private void UpdateSpeeds(float deltaTime)
        {
            float targetRotateSpeed = ShipInput.Rotate switch
            {
                > 0 => _shipSettings.RotateMaxSpeed,
                < 0 => -_shipSettings.RotateMaxSpeed,
                _ => 0
            };

            float maxRotateDelta = _shipSettings.RotateAcceleration * deltaTime;
            RotateSpeed = Mathf.MoveTowards(RotateSpeed, targetRotateSpeed, maxRotateDelta);
            
            
            float targetForwardSpeed = ShipInput.Move switch
            {
                > 0 => _shipSettings.ForwardMaxSpeed,
                < 0 => -_shipSettings.BackwardMaxSpeed,
                _ => 0
            };

            float maxForwardSpeedDelta = _shipSettings.ForwardAcceleration * deltaTime;
            ForwardSpeed = Mathf.MoveTowards(ForwardSpeed, targetForwardSpeed, maxForwardSpeedDelta);
            
            float targetStrafeSpeed = ShipInput.Strafe switch
            {
                > 0 => _shipSettings.StrafeMaxSpeed,
                < 0 => -_shipSettings.StrafeMaxSpeed,
                _ => 0
            };

            float maxStrafeSpeedDelta = _shipSettings.StrafeAcceleration * deltaTime;
            StrafeSpeed = Mathf.MoveTowards(StrafeSpeed, targetStrafeSpeed, maxStrafeSpeedDelta);
        }

        private void UpdatePosition(float deltaTime)
        {
            Angle += -RotateSpeed * deltaTime;

            float radianAngle = Mathf.Deg2Rad * Angle;
            float sin = Mathf.Sin(radianAngle);
            float cos = Mathf.Cos(radianAngle);

            Vector2 position = Position;
            position.x += ForwardSpeed * cos * deltaTime;
            position.y += ForwardSpeed * sin * deltaTime;
            position.x += StrafeSpeed * sin * deltaTime;
            position.y -= StrafeSpeed * cos * deltaTime;
            Position = position;
        }

        public void Destroy()
        {
            Object.Destroy(_view.gameObject);
        }

        private void UpdateView()
        {
            _view.transform.position = Position;
            _view.transform.rotation = Quaternion.Euler(0, 0, Angle);
        }
    }
}