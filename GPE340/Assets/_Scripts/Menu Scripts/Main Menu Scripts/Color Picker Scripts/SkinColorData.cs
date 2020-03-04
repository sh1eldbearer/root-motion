using System;
using TMPro;
using UnityEngine;


[Serializable]
public struct SkinColorData
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The name of this color."),
     SerializeField] private ColorNames _name;
    [Tooltip("The RGB value associated with this color. (Used for UI display.)"),
     SerializeField] private Color _color;
    [Tooltip("The character model to use when this color is selected by a player."),
     SerializeField] private GameObject _model;
    [Tooltip("The avatar for this character model."),
     SerializeField] private Avatar _modelAvatar;
#pragma warning restore CS0649
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
    #endregion
}
