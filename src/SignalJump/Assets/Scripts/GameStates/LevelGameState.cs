namespace SignalJump
{
    public sealed class LevelGameState : IGameState
    {
        private readonly LevelStateMachine _levelStateMachine;
        private readonly LevelWindow _levelWindow;

        public LevelGameState(LevelStateMachine levelStateMachine, LevelWindow levelWindow)
        {
            _levelStateMachine = levelStateMachine;
            _levelWindow = levelWindow;
        }

        public void Enter()
        {
            _levelWindow.ShowWindow();
            _levelStateMachine.EnterToState<IntroLevelState>();
        }

        public void Exit()
        {
            _levelWindow.HideWindow();
        }

        public void Dispose()
        {
        }
    }
}