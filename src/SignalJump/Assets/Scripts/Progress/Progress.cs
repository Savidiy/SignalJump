using System;
using System.Collections.Generic;

namespace SignalJump
{
    [Serializable]
    public sealed class Progress
    {
        public int SelectedLevelIndex = 0;
        public int AvailableLevelIndex = 0;
        public int LevelsCount = 0;
        public bool IsGameCompleted;
        public List<int> CompletedLevels = new List<int>();
        public List<int> SkippedLevels = new List<int>();

        public bool IsAvailableLevel(int index)
        {
            return !IsGameCompleted && AvailableLevelIndex == index;
        }

        public bool IsSkippedLevel(int index)
        {
            return SkippedLevels.Contains(index);
        }

        public bool IsCompletedLevel(int index)
        {
            return CompletedLevels.Contains(index);
        }

        public bool CanSkipLevel(int index)
        {
            return !SkippedLevels.Contains(index) && !CompletedLevels.Contains(index) && index >= 0 && index < LevelsCount;
        }

        public void SkipSelectedLevel()
        {
            SkippedLevels.Add(SelectedLevelIndex);
            CorrectState();
        }

        public void CompleteSelectedLevel()
        {
            if (SkippedLevels.Contains(SelectedLevelIndex))
                SkippedLevels.Remove(SelectedLevelIndex);

            if (!CompletedLevels.Contains(SelectedLevelIndex))
            {
                CompletedLevels.Add(SelectedLevelIndex);
            }

            CorrectState();
        }

        private void CorrectState()
        {
            AvailableLevelIndex = CompletedLevels.Count + SkippedLevels.Count;
            SelectedLevelIndex = AvailableLevelIndex;
            
            if (AvailableLevelIndex >= LevelsCount)
            {
                if (SkippedLevels.Count > 0)
                {
                    SelectedLevelIndex = SkippedLevels[0];
                }
            }

            IsGameCompleted = CompletedLevels.Count == LevelsCount;
        }
    }
}