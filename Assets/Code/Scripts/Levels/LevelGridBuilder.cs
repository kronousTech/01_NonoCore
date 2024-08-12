using UnityEngine;

namespace KronosTech.Levels
{
    public class LevelGridBuilder : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private LevelSquare _squarePrefab;
        [SerializeField] private LevelSquareCounter _counterPrefab;
        
        private LevelSquareCounter[] _columnCounters;
        private LevelSquareCounter[] _rowCounters;
        private LevelSquare[,] _squares;

        private void Start()
        {
            var gridScreenWidth = (transform as RectTransform).rect.width;
            var gridScreenHeight = (transform as RectTransform).rect.height;

            var xSize = LevelBuildSettings.SelectedLevel.GetLength(0);
            var ySize = LevelBuildSettings.SelectedLevel.GetLength(1);
            var size = gridScreenWidth / (xSize + 1);
            var startX = -(gridScreenWidth / 2f);
            var startY = gridScreenHeight / 2f;

            _columnCounters = new LevelSquareCounter[xSize];
            _rowCounters = new LevelSquareCounter[ySize];
            _squares = new LevelSquare[xSize, ySize];

            for (int x = 0; x < xSize; x++)
            {
                // Add columns counter
                _columnCounters[x] = Instantiate(_counterPrefab, Vector2.zero, Quaternion.identity, transform);
                _columnCounters[x].Initialize(LevelSquareCounterType.Column, x);
                _columnCounters[x].GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
                _columnCounters[x].GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + (size * (x + 1)), startY);

                for (int y = 0; y < ySize; y++)
                {
                    // Add square
                    _squares[x,y] = Instantiate(_squarePrefab, Vector2.zero, Quaternion.identity, transform);
                    _squares[x,y].Initialize(x, y);
                    _squares[x,y].name = "Square: " + x + "-" + y;
                    _squares[x,y].GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
                    _squares[x,y].GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + (size * (x + 1)), startY - (size * (y + 1)));

                    if(x == 0)
                    {
                        // Add row counters
                        _rowCounters[y] = Instantiate(_counterPrefab, Vector2.zero, Quaternion.identity, transform);
                        _rowCounters[y].Initialize(LevelSquareCounterType.Row, y);
                        _rowCounters[y].GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
                        _rowCounters[y].GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + (size * x), startY - (size * (y + 1)));
                    }
                }
            }
        }
    }
}