using System;
using BountyHunter.Utils;
using UnityEngine;

namespace BountyHunter
{
    public sealed class BackgroundMover : IDisposable
    {
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly Settings _settings;
        private readonly SpaceTexture _spaceTexture;

        public BackgroundMover(TickInvoker tickInvoker, PlayerHolder playerHolder, Settings settings, SpaceTexture spaceTexture)
        {
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
            _settings = settings;
            _spaceTexture = spaceTexture;
        }

        public void Activate()
        {
            _tickInvoker.Updated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
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
            Vector2 shipPosition = ship.Position;

            float backgroundStep = _settings.BackgroundStep;
            Vector3 spacePosition = _spaceTexture.transform.position;

            float deltaX = Mathf.Abs(shipPosition.x - spacePosition.x);
            float deltaY = Mathf.Abs(shipPosition.y - spacePosition.y);

            if (deltaX < backgroundStep / 2 && deltaY < backgroundStep / 2)
                return;

            spacePosition.x = Mathf.Round(shipPosition.x / backgroundStep) * backgroundStep;
            spacePosition.y = Mathf.Round(shipPosition.y / backgroundStep) * backgroundStep;
            _spaceTexture.transform.position = spacePosition;
        }
    }
}