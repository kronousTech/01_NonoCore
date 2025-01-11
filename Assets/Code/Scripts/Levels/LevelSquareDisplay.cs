using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Levels
{
    public class LevelSquareDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image _backgroundDisplay;
        [SerializeField] private Image _foregroundDisplay;
        [SerializeField] private Image _frameDisplay;
        [SerializeField] private Image _pointDisplay;
        [SerializeField] private TextMeshProUGUI _display;

        private LevelSquare _square;

        private void OnEnable()
        {
            _square.OnInitialize.AddListener(Initialize);
            _square.OnInteract.AddListener(OnInteract);
        }
        private void OnDisable()
        {
            _square.OnInitialize.RemoveListener(Initialize);
            _square.OnInteract.RemoveListener(OnInteract);
        }
        private void Awake()
        {
            _square = GetComponent<LevelSquare>();
        }

        private void Initialize(sbyte value, Vector2 position, Vector2 size)
        {
            //_display.text = Debug.isDebugBuild ? type.ToString() : string.Empty;
            transform.name = "Square: " + position.x + "-" + position.y;
            _pointDisplay.sprite = LevelSpriteManager.GetSquarePoint(value);
            _backgroundDisplay.sprite = LevelSpriteManager.GetSquareBackground();
            _frameDisplay.sprite = LevelSpriteManager.GetSquareFrame(position, (int)size.x, (int)size.y);
        }
        private void OnInteract(sbyte value)
        {
            _pointDisplay.sprite = LevelSpriteManager.GetSquarePoint(value);
        }
    }
}