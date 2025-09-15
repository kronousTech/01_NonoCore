using System;
using UnityEngine;

namespace KronosTech.Levels
{
    public class LevelEndGameListener : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] LevelGridBuilder m_builder;

        public event Action OnGameEnd;

        private void OnEnable()
        {
            m_builder.OnLevelGridBuilt += OnLevelGridBuiltCallback;
        }
        private void OnDisable()
        {
            m_builder.OnLevelGridBuilt -= OnLevelGridBuiltCallback;
        }

        private void OnLevelGridBuiltCallback(LevelGridBuilderEventArgs gridArgs)
        {
            foreach (var square in gridArgs.Squares)
            {
                square.OnInteract += (squareArgs) => CheckGameEndCallback(gridArgs, squareArgs);
            }
        }
        private void CheckGameEndCallback(LevelGridBuilderEventArgs gridArgs, LevelSquareInteractEventArgs squareArgs)
        {
            if(squareArgs.ForcedInteract)
            {
                return;
            }

            foreach (var column in gridArgs.ColumnCounters)
            {
                if (!column.MatchesTargetValue())
                {
                    return;
                }
            }

            foreach (var row in gridArgs.RowCounters)
            {
                if (!row.MatchesTargetValue())
                {
                    return;
                }
            }

            Debug.LogWarning("Level Finished");

            OnGameEnd?.Invoke();
        }
    }
}