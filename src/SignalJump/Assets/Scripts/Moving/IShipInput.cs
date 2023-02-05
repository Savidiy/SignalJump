namespace BountyHunter
{
    public interface IShipInput
    {
        float Move { get; }
        float Rotate { get; }
        float Strafe { get; }
        bool FirePressed { get; }
    }
}