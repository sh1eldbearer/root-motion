using System;
using TMPro;
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
    #region Private Properties
    [Tooltip("The name of this color."),
     SerializeField]
    private ColorNames _name;
    [Tooltip("The RGB value associated with this color. (Used for UI display.)"),
     SerializeField]
    private Color _color;
    [Tooltip("The character model to use when this color is selected by a player."),
     SerializeField]
    private GameObject _model;
    [Tooltip("The avatar for this character model."),
     SerializeField]
    private Avatar _modelAvatar;
    [Tooltip("Whether or not this skin color has been selected by a player."),
     SerializeField]
    private bool _isSelected;
    #endregion

    #region Public Properties
    /// <summary>
    /// The name of this color.
    /// </summary>
    public ColorNames Name
    {
        get { return _name; }
    }

    /// <summary>
    /// The RGB value associated with this color. (Used for UI display.)
    /// </summary>
    public Color Color
    {
        get { return _color; }
    }

    /// <summary>
    /// The character model to use when this color is selected by a player.
    /// </summary>
    public GameObject Model
    {
        get { return _model; }
    }

    /// <summary>
    /// The avatar for this character model.
    /// </summary>
    public Avatar ModelAvatar
    {
        get { return _modelAvatar; }
    }

    /// <summary>
    /// Whether or not this skin color has been selected by a player.
    /// </summary>
    public bool IsSelected
    {
        get { return _isSelected; }
    }

    #endregion


    public void SetSelected(bool isSelected)
    {
        this._isSelected = isSelected;
    }
}
