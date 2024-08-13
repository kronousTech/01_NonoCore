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
        [SerializeField] private Image _frameDisplay;
        [SerializeField] private TextMeshProUGUI _display;

        public void Initialize(int x, int y, int maxX, int maxY)
        {
            var type = LevelBuildSettings.GetSquareType(x, y);
            _display.text = type.ToString();

            transform.name = "Square: " + x + "-" + y;

            _backgroundDisplay.sprite = LevelSpriteManager.GetSquareBackground();
            _foregroundDisplay.sprite = LevelSpriteManager.GetSquareForeground(type);
            _foregroundDisplay.color = _foregroundDisplay.sprite == null ? Color.clear : Color.white;
            _frameDisplay.sprite = LevelSpriteManager.GetSquareFrame(x, y, maxX, maxY);
        }
    }
}