using System;
using UnityEngine;

public enum ColorNames
{
    Disabled = 0,
    Pink = 1,
    Red = 2,
    Orange = 3,
    Gold = 4,
    Green = 5,
    Aqua = 6,
    Blue = 7,
    Violet = 8
}

[Serializable]
public struct ColorData
{
    [Tooltip("The name of this color.")] public ColorNames name;
    [Tooltip("The RGB value associated with this color. (Used for UI display.)")] public UnityEngine.Color color;
    [Tooltip("The character model to use when this color is selected by a player.")] public GameObject model;
    [Tooltip("The avatar for this character model.")] public Avatar modelAvatar;
    [Tooltip("Whether or not this skin color has been selected by a player.")] public bool isSelected;

    public void SetSelected(bool isSelected)
    {
        this.isSelected = isSelected;
    }
}
