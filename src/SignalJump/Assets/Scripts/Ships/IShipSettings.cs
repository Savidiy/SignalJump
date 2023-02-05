namespace BountyHunter
{
    public interface IShipSettings
    {
        float RotateMaxSpeed { get; }
        float RotateAcceleration { get; }
        float StrafeMaxSpeed { get; }
        float StrafeAcceleration { get; }
        float ForwardMaxSpeed { get; }
        float ForwardAcceleration { get; }
        float BackwardMaxSpeed { get; }
    }
}