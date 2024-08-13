using UnityEngine;
using UnityEngine.Events;

namespace KronosTech.Levels
{
    public class LevelSquareCounter : MonoBehaviour
    {
        private LevelSquareCounterDisplay _display;

        private LevelSquareCounterType _type;
        private int _index;
        private int _maxSize;
        private int _targetValue;
        private int _currentValue;

        [HideInInspector] public UnityEvent<bool> OnValueCheck = new();

        private void OnEnable()
        {
            LevelGridBuilder.OnLevelGridBuilt.AddListener(AddListenerToSquares);
        }
        private void OnDisable()
        {
            LevelGridBuilder.OnLevelGridBuilt.RemoveListener(AddListenerToSquares);
        }
        private void Awake()
        {
            _display = GetComponent<LevelSquareCounterDisplay>();
        }

        public void Initialize(LevelSquareCounterType type, int index, int maxSize)
        {
            transform.name = type.ToString() + " counter - " + index.ToString();

            _type = type;
            _index = index;
            _maxSize = maxSize;

            switch (type)
            {
                case LevelSquareCounterType.Column:
                    _targetValue = LevelStateController.GetColumnAnswerValue(index);
                    break;
                case LevelSquareCounterType.Row:
                    _targetValue = LevelStateController.GetRowAnswerValue(index);
                    break;
                default:
                    break;
            }

            _display.Initialize(type, _targetValue, index, maxSize);
        }

        private void AddListenerToSquares(LevelSquare[,] squares, LevelSquareCounter[] columns, LevelSquareCounter[] rows)
        {
            for (int i = 0; i < _maxSize; i++)
            {
                if(_type == LevelSquareCounterType.Column)
                {
                    squares[_index, i].OnInteract.AddListener(CalculateCurrentTotal);
                }
                else
                {
                    squares[i, _index].OnInteract.AddListener(CalculateCurrentTotal);
                }
            }
        }
        private void CalculateCurrentTotal(LevelGridSquareType type)
        {
            _currentValue = _type == LevelSquareCounterType.Column 
                ? LevelStateController.GetColumnValueCount(_index) 
                : LevelStateController.GetRowValueCount(_index);

            Debug.Log(_type.ToString() + "-" + _index.ToString() + "-" + MatchesTargetValue());

            OnValueCheck?.Invoke(MatchesTargetValue());
        }

        public bool MatchesTargetValue()
        {
            return _targetValue == _currentValue;
        }
    }
}