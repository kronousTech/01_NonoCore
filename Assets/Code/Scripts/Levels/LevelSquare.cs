using System;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Levels
{
    public class LevelSquare : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform m_rectTransform;
        [SerializeField] private Button m_button;
        [Header("Debug")]
        [SerializeField] private sbyte _currentValue;
        [SerializeField] private Vector2 _position;

        private sbyte CurrentValue
        {
            get => _currentValue;
            set => _currentValue = (sbyte)((value + 5) % 5);
        }

        public event Action<sbyte, Vector2, Vector2> OnInitialize;
        public event Action<sbyte, bool> OnInteract;

        private void OnEnable()
        {
            m_button.onClick.AddListener(OnButtonClickCallback);
        }
        private void OnDisable()
        {
            m_button.onClick.RemoveListener(OnButtonClickCallback);
        }

        public void Initialize(int x, int y, sbyte value, int maxX, int maxY)
        {
            _position = new Vector2(x, y);
            transform.name = "Square: " + _position.x + "-" + _position.y;

            if(value >= 0)
            {
                CurrentValue = 0;
            }
            else
            {
                _currentValue = -1;
            }

            m_button.interactable = value >= 0;

            OnInitialize?.Invoke(_currentValue, _position, new Vector2(maxX, maxY));
        }

        private void OnButtonClickCallback()
        {
            CurrentValue++;

            OnInteract?.Invoke(CurrentValue, false);
        }

        public bool TryGetValue(out sbyte value)
        { 
            if(CurrentValue == -1)
            {
                value = 0;

                return false;
            }
            else
            {
                value = CurrentValue;

                return true;
            }
        }
        public void ResetValue()
        {
            if(CurrentValue != -1)
            {
                CurrentValue = 0;
                OnInteract?.Invoke(CurrentValue, true);
            }
        }
    }
}