using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SignalJump.Shelter
{
    internal class LevelButtonView : IDisposable
    {
        private readonly int _index;
        private readonly LevelButtonHierarchy _hierarchy;
        private Settings _settings;
        private Progress _progress;

        public event Action<int> SelectIndex;

        public LevelButtonView(int index, LevelButtonHierarchy hierarchy, Settings settings, Progress progress)
        {
            _index = index;
            _hierarchy = hierarchy;
            _settings = settings;
            _progress = progress;

            _hierarchy.LevelLabel.text = (index + 1).ToString();
            _hierarchy.Button.onClick.AddListener(OnClick);
            UpdateColors();
        }

        public void UpdateView(Settings settings, Progress progress)
        {
            _settings = settings;
            _progress = progress;
            UpdateColors();
        }

        public void Dispose()
        {
            _hierarchy.Button.onClick.RemoveListener(OnClick);
            Object.Destroy(_hierarchy);
        }

        private void OnClick()
        {
            SelectIndex?.Invoke(_index);
        }

        private void UpdateColors()
        {
            Color backColor = _settings.LockedLevelColor;
            Color selectedColor = _settings.LockedLevelColor;

            if (_progress.IsAvailableLevel(_index))
                backColor = selectedColor = _settings.AvailableLevelColor;
            else if (_progress.IsSkippedLevel(_index))
                backColor = selectedColor = _settings.SkippedLevelColor;
            else if (_progress.IsCompletedLevel(_index))
                backColor = selectedColor = _settings.CompletedLevelColor;

            if (_progress.SelectedLevelIndex == _index)
                selectedColor = _settings.SelectedLevelColor;

            _hierarchy.BackgroundImage.color = backColor;
            _hierarchy.SelectedImage.color = selectedColor;
        }
    }
}