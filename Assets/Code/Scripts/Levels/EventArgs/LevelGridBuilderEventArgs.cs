namespace KronosTech.Levels
{
    public readonly struct LevelGridBuilderEventArgs
    {
        public LevelSquareCounter[] ColumnCounters { get; }
        public LevelSquareCounter[] RowCounters { get; }
        public LevelSquare[,] Squares { get; }

        public LevelGridBuilderEventArgs(
            LevelSquareCounter[] columnCounters,
            LevelSquareCounter[] rowCounters,
            LevelSquare[,] squares)
        {
            ColumnCounters = columnCounters;
            RowCounters = rowCounters;
            Squares = squares;
        }
    }
}