using System.IO;
using UnityEditor;
using UnityEngine;

namespace KronosTech.Levels.Creation
{
    public class LevelCreator : EditorWindow
    {
        [Header("Settings")]
        [SerializeField][Range(1, 4)] private sbyte m_maxPointsPerSquare;
        [SerializeField][Range(2, 10)] private int m_sizeX = 4;
        [SerializeField][Range(2, 10)] private int m_sizeY = 4;
        [SerializeField] private int m_quantity;
        [SerializeField][Range(0, 50)] private int m_voidSquaresChance;
        [SerializeField][Range(0, 50)] private int m_emptySquaresChance;
        [SerializeField] private string k_savePath = "Assets/LevelCreatorLevels";

        [MenuItem("KronosTech/Tools/Level Creator")]
        public static void ShowWindow()
        {
            GetWindow<LevelCreator>("Level Creator");
        }

        private void OnGUI()
        {
            GUILayout.Space(5);
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            GUILayout.Space(5);
            m_maxPointsPerSquare = (sbyte)EditorGUILayout.IntSlider("Max Points Per Square", m_maxPointsPerSquare, 1, 4);
            m_sizeX = EditorGUILayout.IntSlider("Grid Size X", m_sizeX, 2, 10);
            m_sizeY = EditorGUILayout.IntSlider("Grid Size Y", m_sizeY, 2, 10);
            m_quantity = EditorGUILayout.IntField("Quantity", m_quantity);
            m_voidSquaresChance = EditorGUILayout.IntSlider("Void Squares Chance", m_voidSquaresChance, 0, 50);
            m_emptySquaresChance = EditorGUILayout.IntSlider("Empty Squares Chance", m_emptySquaresChance, 0, 50);
            k_savePath = EditorGUILayout.TextField("Save Path", k_savePath);
            GUILayout.Space(10);

            if (GUILayout.Button("Generate Levels"))
            {
                GenerateLevels();
            }
        }

        private void GenerateLevels()
        {
            if (!AssetDatabase.IsValidFolder(k_savePath))
            {
                Directory.CreateDirectory(k_savePath);
                AssetDatabase.Refresh();
            }

            string path;
            string levelFileName;

            for (int i = 0; i < m_quantity; i++)
            {
                var newLevel = ScriptableObject.CreateInstance<LevelDataScriptableObject>();
                newLevel.Initialize(m_maxPointsPerSquare, new sbyte[m_sizeX, m_sizeY]);

                for (int x = 0; x < m_sizeX; x++)
                {
                    for (int y = 0; y < m_sizeY; y++)
                    {
                        newLevel.Grid[x, y] = GetSquareValue();
                    }
                }

                levelFileName = $"{m_maxPointsPerSquare}-{m_sizeY}x{m_sizeY}-{newLevel.ID}.asset";
                path = Path.Combine(k_savePath, levelFileName);

                AssetDatabase.CreateAsset(newLevel, path);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        private sbyte GetSquareValue()
        {
            var random = Random.Range(0, 100);

            if (random < m_voidSquaresChance)
            {
                return (sbyte)-1;
            }
            else if (random < m_voidSquaresChance + m_emptySquaresChance)
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