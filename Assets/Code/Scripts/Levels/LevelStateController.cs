using System;
using UnityEngine;
using UnityEngine.Events;

namespace KronosTech.Levels
{
    public static class LevelStateController
    {
        private static sbyte[,] SelectedLevel;

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
            SelectedLevel = new sbyte[,]
            {
                { +0, +0, -1, +0 },
                { +1, +2, +0, +0 },
                { -1, +1, +4, +3 },
                { +0, +1, +2, +0 }
            };

            ColumnTotals = GetSelectedLevelColumnsValue();
            RowTotals = GetSelectedLevelRowsValue();

            LevelGridBuilder.OnLevelGridBuilt.AddListener(AddLevelElements);
            LevelGridBuilder.OnLevelGridBuilt.AddListener(AddCheckForGameEnd);
        }

        private static void AddLevelElements(LevelSquare[,] squares, LevelSquareCounter[] columns, LevelSquareCounter[] rows)
        {
            LevelSquares = squares;
            CounterColumns = columns;
            CounterRows = rows;
        }
        private static void AddCheckForGameEnd(LevelSquare[,] squares, LevelSquareCounter[] columns, LevelSquareCounter[] rows)
        {
            foreach (var column in columns)
            {
                column.OnValueCheck += (s) => CheckForGameEnd();
            }
            foreach (var row in rows)
            {
                row.OnValueCheck += (s) => CheckForGameEnd();
            }
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

        #region Values
        public static int GetColumnAnswerValue(int index)
        {
            return ColumnTotals[index];
        }
        public static int GetRowAnswerValue(int index)
        {
            return RowTotals[index];
        }

        public static int GetCurrentColumnValue(int index)
        {
            var total = 0;

            for (int y = 0; y < GetLevelSizeY(); y++)
            {
                total += LevelSquares[y, index].GetValue();
            }

            return total;
        }
        public static int GetCurrentRowValue(int index)
        {
            var total = 0;

            for (int x = 0; x < GetLevelSizeX(); x++)
            {
                 total += LevelSquares[index, x].GetValue();
            }

            return total;
        }

        private static int GetColumnValueCount(int index, sbyte[,] squares)
        {
            var total = 0;

            for (int y = 0; y < GetLevelSizeY(); y++)
            {
                // Inverted x and y
                if (squares[y, index] != -1)
                {
                    total += squares[y, index];
                }
            }

            return total;
        }
        private static int GetRowValueCount(int index, sbyte[,] squares)
        {
            var total = 0;

            for (int x = 0; x < GetLevelSizeX(); x++)
            {
                // Inverted x and y
                if (squares[index, x] != -1)
                {
                    total += squares[index, x];
                }
            }

            return total;
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
        public static sbyte GetSelectedLevelSquare(int x, int y)
        {
            return SelectedLevel[y, x];
        }
        #endregion
    }
}