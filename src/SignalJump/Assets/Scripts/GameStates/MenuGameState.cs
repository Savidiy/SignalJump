namespace BountyHunter
{
    public sealed class MenuGameState : IGameState
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

        public void Dispose()
        {
        }
    }
}