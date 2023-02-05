namespace BountyHunter
{
    public sealed class PlayerHolder
    {
        private readonly ShipFactory _shipFactory;
        private readonly PlayerInput _playerInput;
        private readonly PlayerShipSettings _playerShipSettings;

        public bool HasPlayerShip { get; private set; }
        public Ship PlayerShip { get; private set; }

        public PlayerHolder(ShipFactory shipFactory, PlayerInput playerInput, PlayerShipSettings playerShipSettings)
        {
            _shipFactory = shipFactory;
            _playerInput = playerInput;
            _playerShipSettings = playerShipSettings;
        }

        public void CreatePlayer()
        {
            HasPlayerShip = true;
            Ship ship = _shipFactory.CreateShip(_playerInput, _playerShipSettings);
            PlayerShip = ship;
        }

        public void DestroyPlayer()
        {
            PlayerShip.Destroy();
            PlayerShip = null;
            HasPlayerShip = false;
        }
    }
}