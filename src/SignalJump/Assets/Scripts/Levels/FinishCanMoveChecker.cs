namespace SignalJump
{
    internal sealed class FinishCanMoveChecker : ICanMoveChecker
    {
        public bool CanMoveTo(int myX, int myY, int targetX, int targetY)
        {
            return false;
        }
    }
}