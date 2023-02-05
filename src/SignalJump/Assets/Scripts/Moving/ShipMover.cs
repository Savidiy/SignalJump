using System;
using BountyHunter.Utils;

namespace BountyHunter
{
    public sealed class ShipMover : IDisposable
    {
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;

        public ShipMover(TickInvoker tickInvoker, PlayerHolder playerHolder)
        {
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
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
        }

        private void OnUpdated()
        {
            float deltaTime = _tickInvoker.DeltaTime;

            if (_playerHolder.HasPlayerShip)
                _playerHolder.PlayerShip.UpdateMove(deltaTime);
        }
    }
}