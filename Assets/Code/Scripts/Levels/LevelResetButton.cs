using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Levels
{
    public class LevelResetButton : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LevelGridBuilder _builder;
        [SerializeField] private Button m_button;

        private void OnEnable()
        {
            _builder.OnLevelGridBuilt += OnLevelGridBuiltCallback;
        }
        private void OnDisable()
        {
            _builder.OnLevelGridBuilt -= OnLevelGridBuiltCallback;
        }
        private void OnDestroy()
        {
            m_button.onClick.RemoveAllListeners();
        }

        private void OnLevelGridBuiltCallback(LevelGridBuilderEventArgs args)
        {
            m_button.onClick.AddListener(() => ResetValues(args));
        }
        private void ResetValues(LevelGridBuilderEventArgs args)
        {
            foreach (LevelSquare square in args.Squares)
            {
                square.ResetValue();
            }
        }
    }
}