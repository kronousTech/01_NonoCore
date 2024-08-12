using KronosTech.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
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
