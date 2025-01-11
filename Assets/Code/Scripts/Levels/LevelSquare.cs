using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KronosTech.Levels
{
    public class LevelSquare : MonoBehaviour
    {
        private Vector2 _position;
        private Button _button;

        private sbyte _currentValue;
        private sbyte CurrentValue
        {
            get => _currentValue;
            set => _currentValue = (sbyte)((value + 5) % 5);
        }

        [HideInInspector] public UnityEvent<sbyte, Vector2, Vector2> OnInitialize = new();
        [HideInInspector] public UnityEvent<sbyte> OnInteract = new();

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Initialize(int x, int y, int maxX, int maxY)
        {
            _position = new Vector2(x, y);

            if(LevelStateController.GetSelectedLevelSquare(x, y) == -1)
            {
                _currentValue = -1;
                _button.interactable = false;
            }
            else
            {
                _button.interactable = true;
                CurrentValue = 0;
            }

            OnInitialize?.Invoke(_currentValue, _position, new Vector2(maxX, maxY));
        }

        private void OnButtonClick()
        {
            CurrentValue++;

            OnInteract?.Invoke(CurrentValue);
        }

        public sbyte GetValue()
        { 
            return (sbyte)(CurrentValue == -1 ? 0 : CurrentValue);
        }
    }
}