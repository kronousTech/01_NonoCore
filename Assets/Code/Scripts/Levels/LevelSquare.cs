using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KronosTech.Levels
{
    public class LevelSquare : MonoBehaviour
    {
        private Vector2 _position;
        private Button _button;

        private LevelGridSquareType _selectedLevelSquareType;
        private LevelGridSquareType _currentSquareType;

        [HideInInspector] public UnityEvent<LevelGridSquareType, Vector2, Vector2> OnInitialize = new();
        [HideInInspector] public UnityEvent<LevelGridSquareType, Vector2> OnInteract = new();

        private void OnDisable()
        {
            RemoveButtonListener(_selectedLevelSquareType);
        }
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Initialize(int x, int y, int maxX, int maxY)
        {
            _selectedLevelSquareType = LevelBuildSettings.GetSquareType(x, y);
            _position = new Vector2(x, y);

            _currentSquareType = 
                _selectedLevelSquareType == LevelGridSquareType.Point || _selectedLevelSquareType == LevelGridSquareType.Blank 
                ? LevelGridSquareType.Blank 
                : _selectedLevelSquareType;

            AddButtonListener(_selectedLevelSquareType);

            OnInitialize?.Invoke(_selectedLevelSquareType, _position, new Vector2(maxX, maxY));
        }

        public void AddButtonListener(LevelGridSquareType type)
        {
            if(type == LevelGridSquareType.Blank || type == LevelGridSquareType.Point)
            {
                _button.onClick.AddListener(OnButtonClick);
            }
        }
        private void RemoveButtonListener(LevelGridSquareType type)
        {
            if (type == LevelGridSquareType.Blank || type == LevelGridSquareType.Point)
            {
                _button.onClick.RemoveListener(OnButtonClick);
            }
        }
        private void OnButtonClick()
        {
            if (_currentSquareType == LevelGridSquareType.Point)
            {
                _currentSquareType = LevelGridSquareType.Blank;
            }
            else if(_currentSquareType == LevelGridSquareType.Blank)    
            {
                _currentSquareType = LevelGridSquareType.Point;
            }

            OnInteract?.Invoke(_currentSquareType, _position);
        }
    }
}