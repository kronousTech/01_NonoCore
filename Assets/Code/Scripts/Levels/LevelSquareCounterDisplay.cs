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

        public void Initialize(int count)
        {
            _backgroundDisplay.sprite = LevelSpriteManager.GetCounterBackground();
            _textDisplay.text = count.ToString();
        }
    }
}