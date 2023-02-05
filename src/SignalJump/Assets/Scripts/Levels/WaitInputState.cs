namespace SignalJump
{
    internal class WaitInputState : ILevelState
    {
        private readonly LevelUpdater _levelUpdater;

        public WaitInputState(LevelUpdater levelUpdater)
        {
            _levelUpdater = levelUpdater;
        }

        public void Enter()
        {
            _levelUpdater.Activate();
        }

        public void Exit()
        {
            _levelUpdater.Deactivate();
        }

        public void Dispose()
        {
        }
    }
}