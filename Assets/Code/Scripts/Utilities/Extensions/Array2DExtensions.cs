namespace KronosTech.Utilities.Extensions
{
    public static class Array2DExtensions
    {
        public static T[] GetRow<T>(this T[,] array, int rowIndex)
        {
            int cols = array.GetLength(1);
            T[] row = new T[cols];

            for (int col = 0; col < cols; col++)
            {
                row[col] = array[rowIndex, col];
            }

            return row;
        }

        public static T[] GetColumn<T>(this T[,] array, int colIndex)
        {
            int rows = array.GetLength(0);
            T[] col = new T[rows];

            for (int row = 0; row < rows; row++)
            {
                col[row] = array[row, colIndex];
            }

            return col;
        }
    }
}