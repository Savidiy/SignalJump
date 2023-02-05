using UnityEngine;

namespace BountyHunter
{
    public class ShipFactory
    {
        private readonly Settings _settings;

        public ShipFactory(Settings settings)
        {
            _settings = settings;
        }

        public Ship CreateShip(IShipInput shipInput, PlayerShipSettings playerShipSettings)
        {
            ShipHierarchy shipHierarchy = Object.Instantiate(_settings.ShipPrefab);

            var ship = new Ship(shipHierarchy, shipInput, playerShipSettings);
            return ship;
        }
    }
}