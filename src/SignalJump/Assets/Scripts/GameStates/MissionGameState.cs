namespace BountyHunter
{
    public sealed class MissionGameState : IGameState
    {
        private readonly PlayerHolder _playerHolder;
        private readonly PlayerInput _playerInput;
        private readonly ShipMover _shipMover;
        private readonly CameraMover _cameraMover;
        private readonly BackgroundMover _backgroundMover;

        public MissionGameState(PlayerHolder playerHolder, PlayerInput playerInput, ShipMover shipMover, CameraMover cameraMover,
            BackgroundMover backgroundMover)
        {
            _cameraMover = cameraMover;
            _backgroundMover = backgroundMover;
            _playerHolder = playerHolder;
            _playerInput = playerInput;
            _shipMover = shipMover;
        }

        public void Enter()
        {
            _playerHolder.CreatePlayer();
            _playerInput.Activate();
            _shipMover.Activate();
            _cameraMover.Activate();
            _backgroundMover.Activate();
        }

        public void Exit()
        {
            _backgroundMover.Deactivate();
            _cameraMover.Deactivate();
            _shipMover.Deactivate();
            _playerInput.Deactivate();
            _playerHolder.DestroyPlayer();
        }

        public void Dispose()
        {
        }
    }
}