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

    public static Sprite GetCounterBackground(bool complete)
    {
        return complete ? Sprites[2] : Sprites[1];
    }
    public static Sprite GetSquareBackground()
    {
        return Sprites[0];
    }
    public static Sprite GetSquarePoint(sbyte value)
    {
        if(value == -1)
        {
            return Sprites[3];
        }

        return Sprites[5 + value];
    }
    public static Sprite GetCounterFrame(LevelSquareCounterType type, int index, int maxSize)
    {
        switch (type)
        {
            case LevelSquareCounterType.Column:
                if(index == 0)
                {
                    return Sprites[13];
                }
                else if(index == maxSize-1)
                {
                    return Sprites[15];
                }
                else
                {
                    return Sprites[14];
                }
            case LevelSquareCounterType.Row:
                if (index == 0)
                {
                    return Sprites[18];
                }
                else if (index == maxSize-1)
                {
                    return Sprites[30];
                }
                else
                {
                    return Sprites[24];
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
                return Sprites[19];
            }
            else if(position.x == maxX - 1)
            {
                return Sprites[21];
            }
            else
            {
                return Sprites[20];
            }
        }
        else if(position.y == maxY - 1)
        {
            if (position.x == 0)
            {
                return Sprites[25];
            }
            else if (position.x == maxX - 1)
            {
                return Sprites[27];
            }
            else
            {
                return Sprites[26];
            }
        }
        else
        {
            if (position.x == 0)
            {
                return Sprites[31];
            }
            else if (position.x == maxX - 1)
            {
                return Sprites[33];
            }
            else
            {
                return Sprites[32];
            }
        }
    }
}
