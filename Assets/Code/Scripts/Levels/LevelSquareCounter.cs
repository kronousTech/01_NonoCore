using UnityEngine;

namespace KronosTech.Levels
{
    public class LevelSquareCounter : MonoBehaviour
    {
        private LevelSquareCounterDisplay _display;

        private int _totalValue;

        private void Awake()
        {
            _display = GetComponent<LevelSquareCounterDisplay>();
        }

        public void Initialize(LevelSquareCounterType type, int index)
        {
            transform.name = type.ToString() + " counter - " + index.ToString();

            switch (type)
            {
                case LevelSquareCounterType.Column:
                    _totalValue = LevelBuildSettings.GetColumnValueCount(index);
                    break;
                case LevelSquareCounterType.Row:
                    _totalValue = LevelBuildSettings.GetRowValueCount(index);
                    break;
                default:
                    break;
            }

            _display.Initialize(_totalValue);
        }
    }
}