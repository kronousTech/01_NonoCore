using UnityEngine;

namespace KronosTech.Levels.Skins
{
    public class SkinApplier : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Sprite[] m_sprites;
        [Header("References")]
        [SerializeField] private LevelGridBuilder m_gridBuilder;

        private delegate Sprite GetFrameMethod(int index, int maxSize);

        private void OnEnable()
        {
            m_gridBuilder.OnLevelGridBuilt += OnLevelGridBuiltCallback;
        }
        private void OnDisable()
        {
            m_gridBuilder.OnLevelGridBuilt -= OnLevelGridBuiltCallback;
        }
        private void OnLevelGridBuiltCallback(LevelGridBuilderEventArgs args)
        {
            GetFrameMethod getFrameDelegate = GetColumnCountersFrame;
            AddCountersSkin(args.ColumnCounters, getFrameDelegate);

            getFrameDelegate = GetRowCountersFrame;
            AddCountersSkin(args.RowCounters, getFrameDelegate);

            SkinElementBackground background;
            SkinElementFrame frame;
            Vector2 position;

            for (int x = 0; x < args.Squares.GetLength(1); x++)
            {
                for (int y = 0; y < args.Squares.GetLength(0); y++)
                {
                    background = args.Squares[x, y].GetComponent<SkinElementBackground>();
                    background.SetImageSprite(GetSquareBackground());

                    position.x = x;
                    position.y = y;
                    frame = args.Squares[x, y].GetComponent<SkinElementFrame>();
                    frame.SetImageSprite(GetSquareFrame(position, args.Squares.GetLength(1), args.Squares.GetLength(0)));

                    int indexX = x;
                    int indexY = y;
                    SkinElementSquarePoint point;
                    LevelSquare square;
                    point = args.Squares[x, y].GetComponent<SkinElementSquarePoint>();
                    point.SetImageSprite(GetSquarePoint(args.Squares[x, y]));
                    square = args.Squares[indexX, indexY];
                    args.Squares[indexX, indexY].OnInteract += (args) => OnSquareInteractCallback(square, point);
                }
            }
        }

        private void AddCountersSkin(LevelSquareCounter[] counters, GetFrameMethod getFrameDelegate)
        {
            SkinElementFrame frame;

            for (int i = 0; i < counters.Length; i++)
            {
                SkinElementBackground background = counters[i].GetComponent<SkinElementBackground>();
                background.SetImageSprite(GetCounterBackground(counters[i].MatchesTargetValue()));

                frame = counters[i].GetComponent<SkinElementFrame>();
                frame.SetImageSprite(getFrameDelegate(i, counters.Length));

                int index = i;
                counters[index].OnValueCheck += (match) => OnSquareCounterValueCheck(match, background);
            }
        }
        private void OnSquareCounterValueCheck(bool match, SkinElementBackground background)
        {
            background.SetImageSprite(GetCounterBackground(match));
        }
        private Sprite GetCounterBackground(bool complete)
        {
            return complete ? m_sprites[2] : m_sprites[1];
        }
        private Sprite GetColumnCountersFrame(int index, int maxSize)
        {
            if (index == 0)
            {
                return m_sprites[13];
            }
            else if (index == maxSize - 1)
            {
                return m_sprites[15];
            }
            else
            {
                return m_sprites[14];
            }
        }
        private Sprite GetRowCountersFrame(int index, int maxSize)
        {
            if (index == 0)
            {
                return m_sprites[18];
            }
            else if (index == maxSize - 1)
            {
                return m_sprites[30];
            }
            else
            {
                return m_sprites[24];
            }
        }

        private void OnSquareInteractCallback(LevelSquare square, SkinElementSquarePoint skinElement)
        {
            skinElement.SetImageSprite(GetSquarePoint(square));
        }
        public Sprite GetSquareBackground()
        {
            return m_sprites[0];
        }
        public Sprite GetSquareFrame(Vector2 position, int maxX, int maxY)
        {
            if (position.y == 0)
            {
                if (position.x == 0)
                {
                    return m_sprites[19];
                }
                else if (position.x == maxX - 1)
                {
                    return m_sprites[21];
                }
                else
                {
                    return m_sprites[20];
                }
            }
            else if (position.y == maxY - 1)
            {
                if (position.x == 0)
                {
                    return m_sprites[25];
                }
                else if (position.x == maxX - 1)
                {
                    return m_sprites[27];
                }
                else
                {
                    return m_sprites[26];
                }
            }
            else
            {
                if (position.x == 0)
                {
                    return m_sprites[31];
                }
                else if (position.x == maxX - 1)
                {
                    return m_sprites[33];
                }
                else
                {
                    return m_sprites[32];
                }
            }
        }
        public Sprite GetSquarePoint(LevelSquare square)
        {
            if (square.TryGetValue(out var value))
            {
                return m_sprites[5 + value];
            }
            else
            {
                return m_sprites[3];
            }
        }
    }
}