using SignalJump.Shelter;
using UnityEngine;

namespace SignalJump
{
    [CreateAssetMenu(fileName = "Settings", menuName = "StaticData/Create settings", order = 0)]
    public class Settings : ScriptableObject
    {
        public LevelSequence LevelSequence;
        public LevelButtonHierarchy LevelButtonHierarchyPrefab;

        public int MaxSkippedLevelsCount = 3;
        
        public Color LockedLevelColor;
        public Color AvailableLevelColor;
        public Color CompletedLevelColor;
        public Color SkippedLevelColor;
        public Color SelectedLevelColor;
    }
}