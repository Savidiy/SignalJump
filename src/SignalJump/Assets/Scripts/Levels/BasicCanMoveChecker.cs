namespace SignalJump
{
    internal sealed class BasicCanMoveChecker : ICanMoveChecker
    {
        public bool CanMoveTo(int myX, int myY, int targetX, int targetY)
        {
            if (myX == targetX && myY == targetY + 1) return true;
            if (myX == targetX && myY == targetY + 2) return true;
            if (myX == targetX && myY == targetY - 1) return true;
            if (myX == targetX && myY == targetY - 2) return true;
            if (myX == targetX + 1 && myY == targetY) return true;
            if (myX == targetX + 2 && myY == targetY) return true;
            if (myX == targetX - 1 && myY == targetY) return true;
            if (myX == targetX - 2 && myY == targetY) return true;

            return false;
        }
    }
}