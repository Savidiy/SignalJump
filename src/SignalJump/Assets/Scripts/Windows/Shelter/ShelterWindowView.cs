using System;
using UnityEngine;
using UnityEngine.UI;

namespace SignalJump.Shelter
{
    public sealed class ShelterWindowView : MonoBehaviour
    {
        [SerializeField] private LevelButtonsGrid _levelButtonsGrid;
        [SerializeField] private Button _skipButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _startLevelButton;
        
        private Progress _progress;
        private Settings _settings;

        public event Action MenuClicked;
        public event Action SkipClicked;
        public event Action<int> StartMissionClicked;

        public void ShowWindow(Progress progress, Settings settings)
        {
            _settings = settings;
            _progress = progress;
            _levelButtonsGrid.InitializeView(progress, settings);
            _levelButtonsGrid.SelectedLevelChanged += OnSelectedLevelChanged;
            UpdateFields();
            
            gameObject.SetActive(true);
            AddListeners();
        }

        private void OnSelectedLevelChanged(int index)
        {
            _progress.SelectedLevelIndex = index;
            UpdateFields();
        }

        public void HideWindow()
        {
            gameObject.SetActive(false);
            _skipButton.onClick.RemoveListener(OnSkipClick);
            _menuButton.onClick.RemoveListener(OnMenuClick);
            _startLevelButton.onClick.RemoveListener(OnStartMissionClick);
        }

        private void AddListeners()
        {
            _skipButton.onClick.AddListener(OnSkipClick);
            _menuButton.onClick.AddListener(OnMenuClick);
            _startLevelButton.onClick.AddListener(OnStartMissionClick);
        }

        private void OnMenuClick()
        {
            MenuClicked?.Invoke();
        }

        private void UpdateFields()
        {
            _levelButtonsGrid.UpdateView();
            bool hasAvailableSkips = _progress.SkippedLevels.Count < _settings.MaxSkippedLevelsCount;
            bool canSkipLevel = _progress.CanSkipLevel(_progress.SelectedLevelIndex);
            _skipButton.interactable = hasAvailableSkips && canSkipLevel;

            _startLevelButton.interactable = _progress.SelectedLevelIndex < _settings.LevelSequence.Levels.Count;
        }

        private void OnStartMissionClick()
        {
            StartMissionClicked?.Invoke(_progress.SelectedLevelIndex);
            UpdateFields();
        }

        private void OnSkipClick()
        {            
            if (_progress.SkippedLevels.Count < _settings.MaxSkippedLevelsCount)
                _progress.SkipSelectedLevel();
                
            SkipClicked?.Invoke();
            _levelButtonsGrid.InitializeView(_progress, _settings);
            UpdateFields();
        }
    }
}