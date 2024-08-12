using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Levels
{
    public class LevelSquare : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _backgroundDisplay;
        [SerializeField] private Image _foregroundDisplay;
        [SerializeField] private TextMeshProUGUI _display;

        public void Initialize(int x, int y)
        {
            var type = LevelBuildSettings.GetSquareType(x, y);
            _display.text = type.ToString();

            _backgroundDisplay.sprite = LevelSpriteManager.GetSquareBackground();
            _foregroundDisplay.sprite = LevelSpriteManager.GetSquareForeground(type);
            _foregroundDisplay.color = _foregroundDisplay.sprite == null ? Color.clear : Color.white;
        }
    }
}