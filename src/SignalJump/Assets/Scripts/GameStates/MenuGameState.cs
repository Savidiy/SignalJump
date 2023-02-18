using SignalJump.MainMenu;
using SignalJump.Utils.StateMachine;

namespace SignalJump
{
    public sealed class MenuGameState : IGameState, IState, IStateWithExit
    {
        private readonly MainMenuPresenter _mainMenuPresenter;

        public MenuGameState(MainMenuPresenter mainMenuPresenter)
        {
            _mainMenuPresenter = mainMenuPresenter;
        }

        public void Enter()
        {
            _mainMenuPresenter.ShowWindow();
        }

        public void Exit()
        {
            _mainMenuPresenter.HideWindow();
        }
    }
}