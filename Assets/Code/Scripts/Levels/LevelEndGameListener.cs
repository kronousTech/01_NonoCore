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

        private void OnLevelGridBuiltCallback(LevelGridBuilderEventArgs args)
        {
            foreach (var square in args.Squares)
            {
                square.OnInteract += (value, forced) => CheckGameEndCallback(args);
            }
        }
        private void CheckGameEndCallback(LevelGridBuilderEventArgs args)
        {
            foreach (var column in args.ColumnCounters)
            {
                if (!column.MatchesTargetValue())
                {
                    return;
                }
            }

            foreach (var row in args.RowCounters)
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