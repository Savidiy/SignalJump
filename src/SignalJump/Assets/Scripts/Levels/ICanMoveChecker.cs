namespace SignalJump
{
    public interface ICanMoveChecker
    {
        bool CanMoveTo(int myX, int myY, int targetX, int targetY);
    }
}