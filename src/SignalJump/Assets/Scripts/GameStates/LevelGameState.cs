using SignalJump.Utils.StateMachine;

namespace SignalJump
{
    public sealed class LevelGameState : IGameState, IStateWithPayload<int>, IStateWithExit
    {
        private readonly LevelStateMachine _levelStateMachine;
        private readonly LevelWindow _levelWindow;

        public LevelGameState(LevelStateMachine levelStateMachine, LevelWindow levelWindow)
        {
            _levelStateMachine = levelStateMachine;
            _levelWindow = levelWindow;
        }

        public void Enter(int level)
        {
            _levelWindow.ShowWindow();
            _levelStateMachine.EnterToState<IntroLevelState, int>(level);
        }

        public void Exit()
        {
            _levelWindow.HideWindow();
        }
    }
}