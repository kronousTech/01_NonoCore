using KronosTech.Utilities.Extensions;
using System;
using UnityEngine;

namespace KronosTech.Levels
{
    public class LevelGridBuilder : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DataHolderLevelData m_dataHolder;
        [SerializeField] private RectTransform m_parent;
        [SerializeField] private LevelSquare m_squarePrefab;
        [SerializeField] private LevelSquareCounter m_counterPrefab;

        public event Action<LevelGridBuilderEventArgs> OnLevelGridBuilt;

        private void OnEnable()
        {
            m_dataHolder.OnLevelDataChanged += OnLevelDataChangedCallback;
        }
        private void OnDisable()
        {
            m_dataHolder.OnLevelDataChanged -= OnLevelDataChangedCallback;
        }

        private void OnLevelDataChangedCallback(LevelDataScriptableObject data)
        {
            var gridScreenWidth = m_parent.rect.width;
            var gridScreenHeight = m_parent.rect.height;

            var gridSizeX = data.GetSizeX();
            var gridSizeY = data.GetSizeY();
            var elementSize = gridScreenWidth / (gridSizeX + 1);
            var elementSizeDelta = new Vector2(elementSize, elementSize);
            var startX = -(gridScreenWidth / 2f);
            var startY = gridScreenHeight / 2f;

            // Generate Squares
            var squares = new LevelSquare[gridSizeX, gridSizeY];
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    squares[y, x] = Instantiate(m_squarePrefab, Vector2.zero, Quaternion.identity, m_parent);
                    squares[y, x].Initialize(data.GetValue(x, y), data.MaxPointsPerSquare);
                    squares[y, x].GetComponent<RectTransform>().sizeDelta = elementSizeDelta;
                    squares[y, x].GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + (elementSize * (x + 1)), startY - (elementSize * (y + 1)));
                }
            }

            var columnCounters = new LevelSquareCounter[gridSizeX];
            var rowCounters = new LevelSquareCounter[gridSizeY];
            for (int x = 0; x < gridSizeX; x++)
            {
                // Add columns counter
                columnCounters[x] = Instantiate(m_counterPrefab, Vector2.zero, Quaternion.identity, m_parent);
                columnCounters[x].Initialize(squares.GetColumn(x), data.GetColumnAnswer(x));
                columnCounters[x].GetComponent<RectTransform>().sizeDelta = elementSizeDelta;
                columnCounters[x].GetComponent<RectTransform>().anchoredPosition = new Vector2(startX + (elementSize * (x + 1)), startY);
            }
            for (int y = 0; y < gridSizeY; y++)
            {
                // Add row counters
                rowCounters[y] = Instantiate(m_counterPrefab, Vector2.zero, Quaternion.identity, m_parent);
                rowCounters[y].Initialize(squares.GetRow(y), data.GetRowAnswer(y));
                rowCounters[y].GetComponent<RectTransform>().sizeDelta = elementSizeDelta;
                rowCounters[y].GetComponent<RectTransform>().anchoredPosition = new Vector2(startX, startY - (elementSize * (y + 1)));
            }
            OnLevelGridBuilt?.Invoke(new(columnCounters, rowCounters, squares));
        }
    }
}