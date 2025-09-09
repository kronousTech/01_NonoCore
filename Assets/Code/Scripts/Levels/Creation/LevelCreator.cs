using KronosTech.Levels;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField][Range(1, 4)] private int _difficulty;
    [SerializeField][Range(2, 10)] private int _size;
    [SerializeField] private int _quantity;
    [SerializeField][Range(0, 100)] private int _blackSquaresChance;
    [SerializeField][Range(0, 100)] private int _blankSquaresChange;

    [ContextMenu("Generate")]
    private void GenerateLevels() 
    {
        var newLevel = new LevelData(new sbyte[_size, _size]);
        var json = string.Empty;

        for (int i = 0; i < _quantity; i++) 
        {
            for(int x = 0; x < _size; x++)
            {
                for(int y = 0; y < _size; y++)
                {
                    newLevel.Grid[x, y] = GetSquareValue();
                }
            }

            json = JsonConvert.SerializeObject(newLevel);
            // Save to file
            string path = Path.Combine(Application.dataPath, "CreatedLevels", "test-" + _difficulty + "-" + _size + "-" + i + ".json");

            File.WriteAllText(path, json);
        }
    }

    private sbyte GetSquareValue()
    {
        var random = Random.Range(0, 100);

        if(random < _blackSquaresChance)
        {
            return (sbyte)-1;
        }
        else if (random < _blackSquaresChance + _blankSquaresChange) 
        {
            return (sbyte)0;
        }
        else
        {
            return (sbyte)Random.Range(1, _difficulty);
        }
    }
}