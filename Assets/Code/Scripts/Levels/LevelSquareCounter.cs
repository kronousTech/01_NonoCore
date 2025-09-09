using System;
using UnityEngine;

namespace KronosTech.Levels
{
    public class LevelSquareCounter : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] private int m_targetValue;
        [SerializeField] private int m_currentValue;

        public event Action<LevelSquareCounterEventArgs> OnInitialized;
        public event Action<bool> OnValueCheck;

        public void Initialize(LevelSquare[] squaresToCount, int target)
        {
            m_targetValue = target;
            RefreshCurrentValue(squaresToCount);

            foreach (LevelSquare square in squaresToCount)
            {
                square.OnInteract += (value, forced) => OnSquareInteractCallback(squaresToCount);
            }

            OnInitialized?.Invoke(new LevelSquareCounterEventArgs(target));
        }

        public bool MatchesTargetValue()
        {
            return m_currentValue == m_targetValue;
        }

        private void OnSquareInteractCallback(LevelSquare[] squaresToCount)
        {
            RefreshCurrentValue(squaresToCount);

            OnValueCheck?.Invoke(MatchesTargetValue());
        }
        private void RefreshCurrentValue(LevelSquare[] squaresToCount)
        {
            m_currentValue = 0;

            foreach (var square in squaresToCount)
            {
                if(square.TryGetValue(out var value))
                {
                    m_currentValue += value;
                }
            }
        }
    }
}