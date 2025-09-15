using NaughtyAttributes;
using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace KronosTech.Levels.Creation
{
    public class LevelCreator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField][Range(1, 4)] private sbyte m_maxPointsPerSquare;
        [SerializeField][Range(2, 10)] private int _size;
        [SerializeField] private int m_quantity;
        [SerializeField][Range(0, 50)] private int m_voidSquaresChance;
        [SerializeField][Range(0, 50)] private int m_emptySquaresChange;

        private const string k_createFolderPath = "LevelCreatorLevels";

        [ContextMenu("Generate Levels"),
            Button("Generate Levels")]
        private void GenerateLevels()
        {
            var newLevel = new LevelData(m_maxPointsPerSquare, new sbyte[_size, _size]);
            string json;
            string path;

            for (int i = 0; i < m_quantity; i++)
            {
                for (int x = 0; x < _size; x++)
                {
                    for (int y = 0; y < _size; y++)
                    {
                        newLevel.Grid[x, y] = GetSquareValue();
                    }
                }

                json = JsonConvert.SerializeObject(newLevel, Formatting.Indented);
                // Save to file
                path = Path.Combine(Application.dataPath, k_createFolderPath, $"{m_maxPointsPerSquare}-{_size}-{i}.json");

                File.WriteAllText(path, json);

                AssetDatabase.Refresh();

                EditorUtility.DisplayProgressBar("Creating level", $"Level - {i}", (float)(i + 1) / m_quantity);
            }

            EditorUtility.DisplayDialog("Done", "All levels are created", "Ok");

        }

        private sbyte GetSquareValue()
        {
            var random = Random.Range(0, 100);

            if (random < m_voidSquaresChance)
            {
                return (sbyte)-1;
            }
            else if (random < m_voidSquaresChance + m_emptySquaresChange)
            {
                return (sbyte)0;
            }
            else
            {
                return (sbyte)Random.Range(1, m_maxPointsPerSquare+1);
            }
        }
    }
}