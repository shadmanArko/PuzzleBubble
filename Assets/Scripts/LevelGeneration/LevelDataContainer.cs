using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration
{
    [CreateAssetMenu(fileName = "LevelDataContainer", menuName = "Puzzle Bubble/Level Data Container", order = 1)]
    public class LevelDataContainer : ScriptableObject
    {
        [SerializeField] private List<TextAsset> levelData;
        [SerializeField] private string[] strikerData;
        
        public string GetLevelData(int level)
        {
            return levelData[level].text;
        }

        public string GetStrikerData(int level)
        {
            return strikerData[level];
        }
        
    }
}