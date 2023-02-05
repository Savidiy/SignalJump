namespace SignalJump
{
    internal sealed class LevelUpdater
    {
        private readonly LevelHolder _levelHolder;
        private readonly LevelWindow _levelWindow;
        private readonly LevelStateMachine _levelStateMachine;

        public LevelUpdater(LevelHolder levelHolder, LevelWindow levelWindow, LevelStateMachine levelStateMachine)
        {
            _levelHolder = levelHolder;
            _levelWindow = levelWindow;
            _levelStateMachine = levelStateMachine;
        }

        public void Activate()
        {
            _levelWindow.ActivateButtons();
            _levelWindow.BackClicked += OnBackClicked;
            _levelWindow.RestartClicked += OnRestartClicked;
        }

        private void OnRestartClicked()
        {
            _levelStateMachine.EnterToState<RestartLevelState>();
        }

        private void OnBackClicked()
        {
            _levelStateMachine.EnterToState<OutroLevelState>();
        }

        public void Deactivate()
        {
            _levelWindow.DeactivateButtons();
            _levelWindow.BackClicked -= OnBackClicked;
            _levelWindow.RestartClicked -= OnRestartClicked;
        }
    }
}