using System;
using Object = UnityEngine.Object;

namespace BountyHunter
{
    public class ShelterPresenter : IDisposable
    {
        private readonly ShelterWindowView _shelterWindowView;
        private readonly GameProgressProvider _gameProgressProvider;
        private readonly GameStateMachine _gameStateMachine;

        public ShelterPresenter(GameProgressProvider gameProgressProvider, GameStateMachine gameStateMachine)
        {
            _gameProgressProvider = gameProgressProvider;
            _gameStateMachine = gameStateMachine;
            _shelterWindowView = Object.FindObjectOfType<ShelterWindowView>(true);
            _shelterWindowView.HideWindow();
        }

        public void ShowWindow()
        {
            _shelterWindowView.ShowWindow(_gameProgressProvider.Progress);
            AddListeners();
        }

        public void HideWindow()
        {
            _shelterWindowView.HideWindow();
            RemoveListeners();
        }

        public void Dispose()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            _shelterWindowView.MenuClicked += OnMenuClicked;
            _shelterWindowView.StartMissionClicked += OnStartMissionClicked;
        }

        private void RemoveListeners()
        {
            _shelterWindowView.MenuClicked -= OnMenuClicked;
            _shelterWindowView.StartMissionClicked -= OnStartMissionClicked;
        }

        private void OnMenuClicked()
        {
            _gameProgressProvider.SaveProgress();
            _gameStateMachine.EnterToState<MenuGameState>();
        }

        private void OnStartMissionClicked()
        {
            _gameProgressProvider.SaveProgress();
            _gameStateMachine.EnterToState<MissionGameState>();
        }
    }
}