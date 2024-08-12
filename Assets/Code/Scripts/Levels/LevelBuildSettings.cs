namespace KronosTech.Levels
{
    public static class LevelBuildSettings
    {
        public static LevelGridSquareType[,] SelectedLevel =
        {
            {LevelGridSquareType.Blank, LevelGridSquareType.Point, LevelGridSquareType.Blank, LevelGridSquareType.Blank  },
            {LevelGridSquareType.Point, LevelGridSquareType.Point, LevelGridSquareType.Multiplier, LevelGridSquareType.Multiplier  },
            {LevelGridSquareType.Blocked, LevelGridSquareType.Point, LevelGridSquareType.Point, LevelGridSquareType.Blank  },
            {LevelGridSquareType.Point, LevelGridSquareType.Point, LevelGridSquareType.Point, LevelGridSquareType.Blank  }
        };


        public static LevelGridSquareType GetSquareType(int x, int y)
        {
            return SelectedLevel[y, x];
        }

        public static int GetColumnValueCount(int index)
        {
            var total = 0;
            var multiplier = 1;

            for (int i = 0; i < SelectedLevel.GetLength(0); i++)
            {
                switch (SelectedLevel[i, index])
                {
                    case LevelGridSquareType.Point:
                        total++;
                        break;
                    case LevelGridSquareType.Multiplier:
                        multiplier *= 2;
                        break;
                }
            }

            return total * multiplier;
        }
        public static int GetRowValueCount(int index)
        {
            var total = 0;
            var multiplier = 1;

            for (int i = 0; i < SelectedLevel.GetLength(1); i++)
            {
                switch (SelectedLevel[index, i])
                {
                    case LevelGridSquareType.Point:
                        total++;
                        break;
                    case LevelGridSquareType.Multiplier:
                        multiplier *= 2;
                        break;
                }
            }

            return total * multiplier;
        }
    }
}