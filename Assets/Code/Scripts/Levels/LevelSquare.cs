using NaughtyAttributes;
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
        [SerializeField, ReadOnly] private sbyte m_currentValue;
        [SerializeField, ReadOnly] private sbyte m_maxValue;

        private sbyte CurrentValue
        {
            get => m_currentValue;
            set => m_currentValue = (sbyte)((value + (m_maxValue+1)) % (m_maxValue + 1));
        }

        public event Action<LevelSquareInteractEventArgs> OnInteract;

        private void OnEnable()
        {
            m_button.onClick.AddListener(OnButtonClickCallback);
        }
        private void OnDisable()
        {
            m_button.onClick.RemoveListener(OnButtonClickCallback);
        }

        public void Initialize(sbyte generatedAnswerValue, sbyte maxValue)
        {
            m_maxValue = maxValue;

            if (generatedAnswerValue >= 0)
            {
                CurrentValue = 0;
            }
            else
            {
                m_currentValue = -1;
            }


            m_button.interactable = generatedAnswerValue >= 0;
        }

        private void OnButtonClickCallback()
        {
            CurrentValue++;

            OnInteract?.Invoke(new LevelSquareInteractEventArgs(CurrentValue, false));
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

                OnInteract?.Invoke(new LevelSquareInteractEventArgs(CurrentValue, true));
            }
        }
    }
}