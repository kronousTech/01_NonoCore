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


        public static LevelGridSquareType GetSquareType(int x, int y)
        {
            return SelectedLevel[y, x];
        }

        public static int GetColumnValueCount(int index)
        {
            var total = 0;

            for (int i = 0; i < SelectedLevel.GetLength(0); i++)
            {
                switch (SelectedLevel[i, index])
                {
                    case LevelGridSquareType.Point:
                        total++;
                        break;
                }
            }

            return total;
        }
        public static int GetRowValueCount(int index)
        {
            var total = 0;

            for (int i = 0; i < SelectedLevel.GetLength(1); i++)
            {
                switch (SelectedLevel[index, i])
                {
                    case LevelGridSquareType.Point:
                        total++;
                        break;
                }
            }

            return total;
        }
    }
}