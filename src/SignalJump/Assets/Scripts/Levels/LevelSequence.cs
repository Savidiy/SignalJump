using System.Collections.Generic;
using UnityEngine;

namespace SignalJump
{
    [CreateAssetMenu(fileName = "LevelSequence", menuName = "StaticData/Level Sequence", order = 0)]
    public class LevelSequence : ScriptableObject
    {
        public List<LevelData> Levels;
    }
}