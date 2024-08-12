namespace KronosTech.Levels
{
    public static class LevelBuildSettings
    {
        public static LevelGridSquareType[,] SelectedLevel =
        {
            {LevelGridSquareType.Blank, LevelGridSquareType.Point, LevelGridSquareType.Blank },
            {LevelGridSquareType.Blocked, LevelGridSquareType.Point, LevelGridSquareType.Multiplier },
            {LevelGridSquareType.Point, LevelGridSquareType.Point, LevelGridSquareType.Point }
        };
    }
}