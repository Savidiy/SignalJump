using BountyHunter.Utils;
using UnityEngine;

namespace BountyHunter
{
    public class PlayerInput : IShipInput
    {
        private const float DEFAULT_INPUT_VALUE = 1f;
        private readonly TickInvoker _tickInvoker;
        private readonly Settings _settings;
        private bool _isActive;

        public float Move { get; private set; }
        public float Rotate { get; private set; }
        public float Strafe { get; private set; }
        public bool FirePressed { get; private set; }

        public PlayerInput(TickInvoker tickInvoker, Settings settings)
        {
            _tickInvoker = tickInvoker;
            _settings = settings;
        }

        public void Activate()
        {
            _isActive = true;
            _tickInvoker.Updated += OnUpdate;
        }

        public void Deactivate()
        {
            _isActive = false;
            _tickInvoker.Updated -= OnUpdate;
        }

        private void OnUpdate()
        {
            FirePressed = false;
            Move = 0;
            Strafe = 0;
            Rotate = 0;
            
            if (!_isActive)
                return;
            
            FirePressed = IsPressed(_settings.PlayerFireKey, _settings.PlayerFireAlternativeKey);
            
            if (IsPressed(_settings.PlayerMoveForwardKey, _settings.PlayerMoveForwardAlternativeKey)) Move += DEFAULT_INPUT_VALUE;
            if (IsPressed(_settings.PlayerMoveBackwardKey, _settings.PlayerMoveBackwardAlternativeKey)) Move -= DEFAULT_INPUT_VALUE;

            if (IsPressed(_settings.PlayerStrafeRightKey, _settings.PlayerStrafeRightAlternativeKey)) Strafe += DEFAULT_INPUT_VALUE;
            if (IsPressed(_settings.PlayerStrafeLeftKey, _settings.PlayerStrafeLeftAlternativeKey)) Strafe -= DEFAULT_INPUT_VALUE;

            if (IsPressed(_settings.PlayerRotateRightKey, _settings.PlayerRotateRightAlternativeKey)) Rotate += DEFAULT_INPUT_VALUE;
            if (IsPressed(_settings.PlayerRotateLeftKey, _settings.PlayerRotateLeftAlternativeKey)) Rotate -= DEFAULT_INPUT_VALUE;

        }

        private bool IsPressed(KeyCode key, KeyCode alternativeKey)
        {
            return Input.GetKey(key) || Input.GetKey(alternativeKey);
        }
    }
}