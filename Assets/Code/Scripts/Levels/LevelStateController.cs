using System;
using UnityEngine;
using UnityEngine.Events;

namespace KronosTech.Levels
{
    public static class LevelStateController
    {
        private static LevelGridSquareType[,] SelectedLevel;

        // Multipliers
        private static int[] ColumnMultipliers;
        private static int[] RowMultipliers;
        // Values
        private static int[] ColumnTotals; // MAYBE NOT NECESSARY, SPECIALLY IF ONLY CALLED ONCE PER LEVEL
        private static int[] RowTotals;
        // Current state Values
        private static LevelSquareCounter[] CounterColumns; 
        private static LevelSquareCounter[] CounterRows;
        private static LevelSquare[,] LevelSquares;

        public static UnityEvent OnGameEnd = new();

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
            // ADD TO ON LEVEL SELECT
            ColumnMultipliers = GetSelectedLevelColumnsMultiplier();
            RowMultipliers = GetSelectedLevelRowsMultiplier();

            ColumnTotals = GetSelectedLevelColumnsValue();
            RowTotals = GetSelectedLevelRowsValue();

            LevelGridBuilder.OnLevelGridBuilt.AddListener(AddLevelElements);
            LevelSquare.OnCheckForGameEnd.AddListener(CheckForGameEnd);
        }

        private static void AddLevelElements(LevelSquare[,] squares, LevelSquareCounter[] columns, LevelSquareCounter[] rows)
        {
            LevelSquares = squares;
            CounterColumns = columns;
            CounterRows = rows;
        }
        private static void CheckForGameEnd()
        {
            foreach (var column in CounterColumns)
            {
                if (!column.MatchesTargetValue())
                {
                    return;
                }
            }

            foreach (var row in CounterRows)
            {
                if (!row.MatchesTargetValue())
                {
                    return;
                }
            }

            Debug.LogWarning("Level Finished");

            OnGameEnd?.Invoke();
        }

        #region Multipliers
        private static int[] GetSelectedLevelRowsMultiplier()
        {
            var multipliers = new int[GetLevelSizeY()];

            for (int y = 0; y < GetLevelSizeY(); y++)
            {
                var multiplierValue = 1;

                for (int x = 0; x < GetLevelSizeX(); x++)
                {
                    if (GetSelectedLevelSquare(x,y) == LevelGridSquareType.Multiplier)
                    {
                        multiplierValue *= 2;
                    }
                }

                multipliers[y] = multiplierValue;
            }
            

            return multipliers;
        }
        private static int[] GetSelectedLevelColumnsMultiplier()
        {
            var multipliers = new int[GetLevelSizeX()];

            for (int x = 0; x < GetLevelSizeX(); x++)
            {
                var multiplierValue = 1;

                for (int y = 0; y < GetLevelSizeY(); y++)
                {
                    if (GetSelectedLevelSquare(x, y) == LevelGridSquareType.Multiplier)
                    {
                        multiplierValue *= 2;
                    }
                }

                multipliers[x] = multiplierValue;
            }

            return multipliers;
        }
        #endregion

        #region Values
        public static int GetColumnAnswerValue(int index)
        {
            return ColumnTotals[index];
        }
        public static int GetRowAnswerValue(int index)
        {
            return RowTotals[index];
        }
        public static int GetColumnValueCount(int index)
        {
            var total = 0;

            for (int y = 0; y < GetLevelSizeY(); y++)
            {
                if (LevelSquares[index, y].GetCurrentType() == LevelGridSquareType.Point)
                {
                    total += 1 * RowMultipliers[y];
                }
            }

            return total * ColumnMultipliers[index];
        }
        private static int GetColumnValueCount(int index, LevelGridSquareType[,] squares)
        {
            var total = 0;

            for (int y = 0; y < GetLevelSizeY(); y++)
            {
                // Inverted x and y
                if (GetSelectedLevelSquare(index, y) == LevelGridSquareType.Point)
                {
                    total += 1 * RowMultipliers[y];
                }
            }

            return total * ColumnMultipliers[index];
        }
        public static int GetRowValueCount(int index)
        {
            var total = 0;

            for (int x = 0; x < GetLevelSizeX(); x++)
            {
                if (LevelSquares[x, index].GetCurrentType() == LevelGridSquareType.Point)
                {
                    total += 1 * ColumnMultipliers[x];
                }
            }

            return total * RowMultipliers[index];
        }
        private static int GetRowValueCount(int index, LevelGridSquareType[,] squares)
        {
            var total = 0;

            for (int x = 0; x < GetLevelSizeX(); x++)
            {
                // Inverted x and y
                if (GetSelectedLevelSquare(x, index) == LevelGridSquareType.Point)
                {
                    total += 1 * ColumnMultipliers[x];
                }
            }

            return total * RowMultipliers[index];
        }
        private static int[] GetSelectedLevelRowsValue()
        {
            var values = new int[GetLevelSizeY()];

            for (int y = 0; y < GetLevelSizeY(); y++)
            {
                values[y] = GetRowValueCount(y, SelectedLevel);
            }

            return values;
        }
        private static int[] GetSelectedLevelColumnsValue()
        {
            var values = new int[GetLevelSizeX()];

            for (int x = 0; x < GetLevelSizeX(); x++)
            {
                values[x] = GetColumnValueCount(x, SelectedLevel);
            }

            return values;
        }
        #endregion

        #region Getters/Setter
        public static int GetLevelSizeX()
        {
            return SelectedLevel.GetLength(1);
        }
        public static int GetLevelSizeY()
        {
            return SelectedLevel.GetLength(0);
        }
        public static LevelGridSquareType GetSelectedLevelSquare(int x, int y)
        {
            return SelectedLevel[y, x];
        }
        #endregion
    }
}