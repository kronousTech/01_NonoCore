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

        public void Initialize(LevelSquareCounterType type, int count, int index, int maxSize)
        {
            _backgroundDisplay.sprite = LevelSpriteManager.GetCounterBackground();
            _frameDisplay.sprite = LevelSpriteManager.GetCounterFrame(type, index, maxSize);
            _textDisplay.text = count.ToString();
        }
    }
}