using Sirenix.OdinInspector;
using UnityEngine;

namespace SignalJump
{
    [CreateAssetMenu(fileName = "Level_Data", menuName = "StaticData/Level", order = 0)]
    public class LevelData : SerializedScriptableObject
    {
        private const int DEFAULT_X = 3;
        private const int DEFAULT_Y = 5;
        private const int MINIMAL_LEVEL_SIZE = 2;

        public Vector2Int LevelSize = new Vector2Int(DEFAULT_X,DEFAULT_Y);

        [TableMatrix(HorizontalTitle = "X axis", VerticalTitle = "Y axis")]
        public ELevelCellType[,] LevelCells = new ELevelCellType[DEFAULT_X,DEFAULT_Y];

        private void OnValidate()
        {
            if (LevelSize.x < MINIMAL_LEVEL_SIZE) LevelSize.x = MINIMAL_LEVEL_SIZE;
            if (LevelSize.y < MINIMAL_LEVEL_SIZE) LevelSize.y = MINIMAL_LEVEL_SIZE;
            
            if (LevelCells == null) 
                LevelCells = new ELevelCellType[LevelSize.x, LevelSize.y];

            int width = LevelCells.GetLength(0);
            int height = LevelCells.GetLength(1);
            if (width != LevelSize.x ||
                height != LevelSize.y)
            {
                var newLevel = new ELevelCellType[LevelSize.x, LevelSize.y];

                for (int x = 0; x < width && x < LevelSize.x; x++)
                {
                    for (int y = 0; y < height && y < LevelSize.y; y++)
                    {
                        newLevel[x, y] = LevelCells[x, y];
                    }
                }

                LevelCells = newLevel;
            }
        }
    }

    public enum ELevelCellType
    {
        None = 0,
        Basic = 1,
        Start = 2,
        Finish = 3,
    }
}