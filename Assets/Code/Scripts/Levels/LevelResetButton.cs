using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Levels
{
    public class LevelResetButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LevelGridBuilder _builder;

        private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(ResetValues);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(ResetValues);
        }
        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void ResetValues()
        {
            foreach (LevelSquare square in _builder.GetSquares())
            {
                square.ResetValue();
            }
        }
    }
}