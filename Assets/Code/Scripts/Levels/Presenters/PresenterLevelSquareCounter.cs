using TMPro;
using UnityEngine;

namespace KronosTech.Levels
{
    public class PresenterLevelSquareCounter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LevelSquareCounter m_squareCounter;
        [SerializeField] private TextMeshProUGUI _textDisplay;

        private void OnEnable()
        {
            m_squareCounter.OnInitialized += OnInitializedCallback;
        }
        private void OnDisable()
        {
            m_squareCounter.OnInitialized -= OnInitializedCallback;
        }

        private void OnInitializedCallback(LevelSquareCounterEventArgs args)
        {
            _textDisplay.text = args.Target.ToString();
        }
    }
}