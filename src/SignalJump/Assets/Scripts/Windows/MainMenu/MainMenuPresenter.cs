using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SignalJump.MainMenu
{
    public sealed class MainMenuPresenter : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueGameButton;
        [SerializeField] private Button _exitGameButton;
        [SerializeField] private Button _resetGameButton;

        private GameProgressProvider _gameProgressProvider;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameProgressProvider gameProgressProvider, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _gameProgressProvider = gameProgressProvider;
            HideWindow();
        }

        public void ShowWindow()
        {
            InitView();
            gameObject.SetActive(true);
            _newGameButton.onClick.AddListener(OnNewGameClick);
            _continueGameButton.onClick.AddListener(OnContinueGameClick);
            _exitGameButton.onClick.AddListener(OnExitGameClick);
            _resetGameButton.onClick.AddListener(OnResetGameClick);
        }

        private void InitView()
        {
            bool canContinue = _gameProgressProvider.HasSavedProgress || _gameProgressProvider.HasCurrentProgress;
            _newGameButton.gameObject.SetActive(!canContinue);
            _continueGameButton.gameObject.SetActive(canContinue);
            _resetGameButton.gameObject.SetActive(canContinue);
        }

        private void OnNewGameClick()
        {
            _gameProgressProvider.ResetProgressForNewGame();
            _gameProgressProvider.SaveProgress();
            
            _gameStateMachine.EnterToState<ShelterGameState>();
        }

        private void OnContinueGameClick()
        {
            if (!_gameProgressProvider.HasCurrentProgress)
                _gameProgressProvider.LoadProgress();

            _gameStateMachine.EnterToState<ShelterGameState>();
        }

        private void OnExitGameClick()
        {
            _gameStateMachine.EnterToState<ExitGameState>();
        }

        private void OnResetGameClick()
        {
            _gameProgressProvider.ResetProgressForNewGame();
            InitView();
        }

        public void HideWindow()
        {
            gameObject.SetActive(false);
            _newGameButton.onClick.RemoveListener(OnNewGameClick);
            _continueGameButton.onClick.RemoveListener(OnContinueGameClick);
            _exitGameButton.onClick.RemoveListener(OnExitGameClick);
            _resetGameButton.onClick.RemoveListener(OnResetGameClick);
        }
    }
}