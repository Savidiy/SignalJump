using DG.Tweening;
using SignalJump.Shelter;
using UnityEngine;

namespace SignalJump
{
    [CreateAssetMenu(fileName = "Settings", menuName = "StaticData/Create settings", order = 0)]
    public class Settings : ScriptableObject
    {
        public LevelSequence LevelSequence;
        public LevelButtonHierarchy LevelButtonHierarchyPrefab;
        public BasicLevelCell BasicLevelCellPrefab;
        public BasicLevelCell FinishLevelCellPrefab;

        public float CellStep = 3;
        public int MaxSkippedLevelsCount = 3;
        public float ShowCellsDelay = 0.2f;

        public Color LockedLevelColor;
        public Color AvailableLevelColor;
        public Color CompletedLevelColor;
        public Color SkippedLevelColor;
        public Color SelectedLevelColor;

        public Vector3 CellShowOffset;
        public float CellShowDuration;
        public Ease CellShowEasing;

        public float CellFadeDuration;
        public Ease CellFadeEasing;

        public PlayerView PlayerViewPrefab;
        public float PlayerIntroDuration = 1;
        public Ease PlayerIntroEasing;
        public Vector3 PlayerIntroOffset;
        public Ease PlayerOutroEasing;
        public Vector3 PlayerOutroOffset;

        public Color AvailableCellColor = Color.green;
        public Color UnavailableCellColor = Color.grey;
        public float AvailabilitySwapDuration = 0.1f;
        
        public float PlayerMoveDuration = 0.5f;
        public float PlayerMoveHeigth = 0.5f;
        public Ease PlayerMoveXEasing;
        public Ease PlayerMoveInYEasing;
        public Ease PlayerMoveOutYEasing;
        public Ease PlayerMoveZEasing;

    }
}