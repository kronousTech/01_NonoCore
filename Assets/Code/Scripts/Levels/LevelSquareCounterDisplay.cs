using TMPro;
using UnityEngine;

namespace KronosTech.Levels
{
    public class LevelSquareCounterDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _textDisplay;

        public void Initialize(int count)
        {
            _textDisplay.text = count.ToString();
        }
    }
}