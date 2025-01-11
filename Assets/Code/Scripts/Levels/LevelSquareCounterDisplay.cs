using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Levels
{
    public class LevelSquareCounterDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _backgroundDisplay;
        [SerializeField] private TextMeshProUGUI _textDisplay;
        [SerializeField] private Image _frameDisplay;

        private LevelSquareCounter _counter;

        private void OnEnable()
        {
            _counter.OnValueCheck += UpdateDisplayOnComplete;
        }
        private void OnDisable()
        {
            _counter.OnValueCheck -= UpdateDisplayOnComplete;
        }
        private void Awake()
        {
            _counter = GetComponent<LevelSquareCounter>();
        }

        public void Initialize(LevelSquareCounterType type, int count, int index, int maxSize)
        {
            _backgroundDisplay.sprite = LevelSpriteManager.GetCounterBackground(_counter.MatchesTargetValue());
            _frameDisplay.sprite = LevelSpriteManager.GetCounterFrame(type, index, maxSize);
            _textDisplay.text = count.ToString();
        }

        private void UpdateDisplayOnComplete(bool complete) 
        {
            _backgroundDisplay.sprite = LevelSpriteManager.GetCounterBackground(complete);
        }
    }
}