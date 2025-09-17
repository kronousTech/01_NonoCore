using NaughtyAttributes;
using System;
using UnityEngine;

namespace KronosTech.Levels
{
    public class LevelDataScriptableObject : ScriptableObject
    {
        [Header("Debug")]
        [ReadOnly] public string ID;
        [ReadOnly] public sbyte MaxPointsPerSquare;
        [ReadOnly] public sbyte[,] Grid;

        private int[] m_rowsAnswers;
        private int[] m_columnsAnswers;

        public void Initialize(sbyte maxPointsPerSquare, sbyte[,] grid)
        {
            this.ID = Guid.NewGuid().ToString("N");
            this.MaxPointsPerSquare = maxPointsPerSquare;
            this.Grid = grid;

            m_rowsAnswers = GetAnswersRow();
            m_columnsAnswers = GetAnswersColumns();
        }

        public int GetSizeX()
        {
            return Grid.GetLength(1);
        }
        public int GetSizeY()
        {
            return Grid.GetLength(0);
        }
        public int GetColumnAnswer(int index)
        {
            return m_rowsAnswers[index];
        }
        public int GetRowAnswer(int index)
        {
            return m_columnsAnswers[index];
        }
        public sbyte GetValue(int x, int y)
        {
            return Grid[y, x];
        }

        private int[] GetAnswersRow()
        {
            var values = new int[GetSizeY()];

            for (int y = 0; y < GetSizeY(); y++)
            {
                values[y] = GetRowValueCount(y);
            }

            return values;
        }
        private int[] GetAnswersColumns()
        {
            var values = new int[GetSizeX()];

            for (int x = 0; x < GetSizeX(); x++)
            {
                values[x] = GetColumnValueCount(x);
            }

            return values;
        }
        private int GetColumnValueCount(int index)
        {
            var total = 0;

            for (int y = 0; y < GetSizeY(); y++)
            {
                // Inverted x and y
                if (Grid[y, index] != -1)
                {
                    total += Grid[y, index];
                }
            }

            return total;
        }
        private int GetRowValueCount(int index)
        {
            var total = 0;

            for (int x = 0; x < GetSizeX(); x++)
            {
                // Inverted x and y
                if (Grid[index, x] != -1)
                {
                    total += Grid[index, x];
                }
            }

            return total;
        }
    }
}