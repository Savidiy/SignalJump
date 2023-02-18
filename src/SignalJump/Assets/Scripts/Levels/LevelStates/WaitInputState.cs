using SignalJump.Utils.StateMachine;

namespace SignalJump
{
    internal class WaitInputState : ILevelState, IState, IStateWithExit
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
    }
}