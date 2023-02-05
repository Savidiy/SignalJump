namespace BountyHunter
{
    public class PlayerShipSettings : IShipSettings
    {
        private readonly Settings _settings;
        public float RotateMaxSpeed => _settings.RotateMaxSpeed;
        public float RotateAcceleration => _settings.RotateAcceleration;
        public float StrafeMaxSpeed => _settings.StrafeMaxSpeed;
        public float StrafeAcceleration => _settings.StrafeAcceleration;
        public float ForwardMaxSpeed => _settings.ForwardMaxSpeed;
        public float ForwardAcceleration => _settings.ForwardAcceleration;
        public float BackwardMaxSpeed => _settings.BackwardMaxSpeed;

        public PlayerShipSettings(Settings settings)
        {
            _settings = settings;
        }
    }
}