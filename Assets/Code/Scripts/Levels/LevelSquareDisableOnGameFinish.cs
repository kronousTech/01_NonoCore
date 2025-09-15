using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Levels
{
    public class LevelSquareDisableOnGameFinish : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button m_button;

        private LevelEndGameListener m_endGameListener;

        private void OnEnable()
        {
            m_endGameListener.OnGameEnd += OnGameEndCallback;
        }
        private void OnDisable()
        {
            m_endGameListener.OnGameEnd -= OnGameEndCallback;
        }
        private void Awake()
        {
            m_endGameListener = GameObject.FindFirstObjectByType<LevelEndGameListener>();

            if(m_endGameListener == null)
            {
                Debug.LogError($"{nameof(LevelSquareDisableOnGameFinish)}.cs: " +
                    $"Failed to find the {nameof(LevelEndGameListener)} component.");
            }
        }

        private void OnGameEndCallback()
        {
            m_button.interactable = false;
        }
    }
}