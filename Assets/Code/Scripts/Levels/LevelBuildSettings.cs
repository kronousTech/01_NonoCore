using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace KronosTech.Levels
{
    public static class LevelBuildSettings
    {
        public static LevelGridSquareType[,] SelectedLevel;

        private static int[] ColumnMultipliers;
        private static int[] RowMultipliers;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            SelectedLevel = new LevelGridSquareType[,]
            {
                { LevelGridSquareType.Blank, LevelGridSquareType.Multiplier, LevelGridSquareType.Blank, LevelGridSquareType.Blank  },
                { LevelGridSquareType.Point, LevelGridSquareType.Point, LevelGridSquareType.Multiplier, LevelGridSquareType.Multiplier  },
                { LevelGridSquareType.Blocked, LevelGridSquareType.Point, LevelGridSquareType.Point, LevelGridSquareType.Blank  },
                { LevelGridSquareType.Point, LevelGridSquareType.Point, LevelGridSquareType.Point, LevelGridSquareType.Blank  }
            };

            ColumnMultipliers = GetColumnsMultiplier();
            RowMultipliers = GetRowsMultiplier();
        }

        private static int[] GetRowsMultiplier()
        {
            var multipliers = new int[SelectedLevel.GetLength(0)];

            for (int x = 0; x < SelectedLevel.GetLength(1); x++)
            {
                var multiplierValue = 1;

                for(int y = 0; y < SelectedLevel.GetLength(0); y++)
                {
                    if (SelectedLevel[x,y] == LevelGridSquareType.Multiplier)
                    {
                        multiplierValue *= 2;
                    }
                }

                multipliers[x] = multiplierValue;
            }

            return multipliers;
        }
        private static int[] GetColumnsMultiplier()
        {
            var multipliers = new int[SelectedLevel.GetLength(1)];

            for (int y = 0; y < SelectedLevel.GetLength(0); y++)
            {
                var multiplierValue = 1;

                for (int x = 0; x < SelectedLevel.GetLength(1); x++)
                {
                    if (SelectedLevel[x, y] == LevelGridSquareType.Multiplier)
                    {
                        multiplierValue *= 2;
                    }
                }

                multipliers[y] = multiplierValue;
            }

            return multipliers;
        }

        public static LevelGridSquareType GetSquareType(int x, int y)
        {
            return SelectedLevel[y, x];
        }

        public static int GetColumnValueCount(int index)
        {
            var total = 0;

            for (int x = 0; x < SelectedLevel.GetLength(1); x++)
            {
                if (SelectedLevel[x, index] == LevelGridSquareType.Point)
                {
                    total += 1 * RowMultipliers[x];
                }
            }

            return total * ColumnMultipliers[index];
        }
        public static int GetRowValueCount(int index)
        {
            var total = 0;

            for (int y = 0; y < SelectedLevel.GetLength(0); y++)
            {
                if (SelectedLevel[index, y] == LevelGridSquareType.Point)
                {
                    total += 1 * ColumnMultipliers[y];
                }
            }

            return total * RowMultipliers[index];
        }
    }
}