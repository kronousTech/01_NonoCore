using System;

namespace KronosTech.Levels
{
    public class LevelSquareCounterEventArgs : EventArgs
    {
        public int Target { get; }

        public LevelSquareCounterEventArgs(int target)
        {
            this.Target = target;
        }
    }
}