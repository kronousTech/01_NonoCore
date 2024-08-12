using TMPro;
using UnityEngine;

namespace KronosTech.Levels
{
    public class LevelSquare : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI _display;

        public void Initialize(int x, int y)
        {
            _display.text = LevelBuildSettings.GetSquareType(x,y).ToString();
        }
    }
}