using KronosTech.Levels;
using UnityEngine;

public class LevelSpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprite;

    private static Sprite[] Sprites;

    private void Awake()
    {
        Sprites = _sprite;
    }

    public static Sprite GetBackground()
    {
        return Sprites[0];
    }
    public static Sprite GetCounterBackground()
    {
        return Sprites[1];
    }
    public static Sprite GetSquareBackground()
    {
        return Sprites[2];
    }
    public static Sprite GetSquarePoint()
    {
        return Sprites[3];
    }
    public static Sprite GetCounterFrame(LevelSquareCounterType type, int index, int maxSize)
    {
        switch (type)
        {
            case LevelSquareCounterType.Column:
                if(index == 0)
                {
                    return Sprites[7];
                }
                else if(index == maxSize-1)
                {
                    return Sprites[9];
                }
                else
                {
                    return Sprites[8];
                }
            case LevelSquareCounterType.Row:
                if (index == 0)
                {
                    return Sprites[10];
                }
                else if (index == maxSize-1)
                {
                    return Sprites[20];
                }
                else
                {
                    return Sprites[15];
                }
            default:
                Debug.LogError("LevelSpriteManager.cs: Invalid Square counter type: " + type);
                return null;
        }
    }
    public static Sprite GetSquareFrame(Vector2 position, int maxX, int maxY)
    {
        if(position.y == 0)
        {
            if(position.x == 0)
            {
                return Sprites[11];
            }
            else if(position.x == maxX - 1)
            {
                return Sprites[13];
            }
            else
            {
                return Sprites[12];
            }
        }
        else if(position.y == maxY - 1)
        {
            if (position.x == 0)
            {
                return Sprites[21];
            }
            else if (position.x == maxX - 1)
            {
                return Sprites[23];
            }
            else
            {
                return Sprites[22];
            }
        }
        else
        {
            if (position.x == 0)
            {
                return Sprites[16];
            }
            else if (position.x == maxX - 1)
            {
                return Sprites[18];
            }
            else
            {
                return Sprites[17];
            }
        }
    }

    public static Sprite GetSquareForeground(LevelGridSquareType type)
    {
        return type switch
        {
            LevelGridSquareType.Blocked => Sprites[5],
            LevelGridSquareType.Multiplier => Sprites[6],
            _ => null,
        };
    }
}
