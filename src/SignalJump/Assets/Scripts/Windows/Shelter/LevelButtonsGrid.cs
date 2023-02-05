using System;
using System.Collections.Generic;
using UnityEngine;

namespace SignalJump.Shelter
{
    public sealed class LevelButtonsGrid : MonoBehaviour
    {
        [SerializeField] private Transform _gridRoot;

        private Progress _progress;
        private Settings _settings;

        private readonly List<LevelButtonView> _buttonViews = new List<LevelButtonView>();

        public event Action<int> SelectedLevelChanged;

        public void InitializeView(Progress progress, Settings settings)
        {
            _settings = settings;
            _progress = progress;

            CorrectButtonsCount();
            UpdateButtonViews();
        }

        public void UpdateView()
        {
            UpdateButtonViews();
        }

        private void UpdateButtonViews()
        {
            foreach (LevelButtonView levelButtonView in _buttonViews)
            {
                levelButtonView.UpdateView(_settings, _progress);
            }
        }

        private void CorrectButtonsCount()
        {
            int levelsCount = _settings.LevelSequence.Levels.Count;
            for (int i = levelsCount; i < _buttonViews.Count; i++)
                RemoveButtonView(_buttonViews.Count - 1);

            for (int i = _buttonViews.Count; i < levelsCount; i++)
                AddButtonView();
        }

        private void RemoveButtonView(int index)
        {
            LevelButtonView levelButtonView = _buttonViews[index];
            levelButtonView.SelectIndex -= OnSelectIndex;
            levelButtonView.Dispose();
        }

        private void AddButtonView()
        {
            int index = _buttonViews.Count;
            LevelButtonHierarchy levelButtonHierarchy = Instantiate(_settings.LevelButtonHierarchyPrefab, _gridRoot);
            var levelButtonView = new LevelButtonView(index, levelButtonHierarchy, _settings, _progress);
            _buttonViews.Add(levelButtonView);
            levelButtonView.SelectIndex += OnSelectIndex;
        }

        private void OnSelectIndex(int index)
        {
            if (_progress.IsAvailableLevel(index) ||
                _progress.IsCompletedLevel(index) ||
                _progress.IsSkippedLevel(index))
            {
                SelectedLevelChanged?.Invoke(index);
                UpdateButtonViews();
            }
        }
    }
}