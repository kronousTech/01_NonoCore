using System;

namespace KronosTech.Levels
{
    public class LevelSquareInteractEventArgs : EventArgs
    {
        public sbyte Value { get; }
        public bool ForcedInteract { get; }

        public LevelSquareInteractEventArgs(sbyte currentValue, bool forcedInteract)
        {
            Value = currentValue;
            ForcedInteract = forcedInteract;
        }
    }
}