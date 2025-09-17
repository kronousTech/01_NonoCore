using KronosTech.Levels;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.MainMenu.LevelSelector
{
    [CreateAssetMenu(fileName = "LevelsRepository", menuName = "KronosTech/MainMenu/LevelSelector/LevelsRepository")]
    public class LevelsRepositoryScriptableObject : ScriptableObject
    {
        public int MaxPointsPerSquareCategory;
        public List<LevelDataScriptableObject> LevelData;
    }
}