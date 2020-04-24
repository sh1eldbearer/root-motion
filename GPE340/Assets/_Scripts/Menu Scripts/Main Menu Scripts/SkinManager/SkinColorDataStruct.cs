using System;
using TMPro;
using UnityEngine;

[Serializable]
public struct SkinColorDataStruct
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The name of this color."),
        SerializeField] private Enums.ColorNames _name;
    [Tooltip("The RGB value associated with this color. (Used for UI display.)"),
        SerializeField] private Color _color;

    [Tooltip("The avatar to use with character of this skin color."),
        Space, SerializeField] private Avatar _avatar;

    [Header("Materials")]
    [Tooltip("The body material for this skin color."),
        SerializeField] private Material _bodyMaterial;
    [Tooltip("The brows material for this skin color."),
         SerializeField] private Material _browsMaterial;
    [Tooltip("The eye material for this skin color."),
         SerializeField] private Material _eyeMaterial;
    [Tooltip("The eye spec material for this skin color."),
         SerializeField] private Material _eyeSpecMaterial;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The name of this color.
    /// </summary>
    public Enums.ColorNames Name
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
    /// The avatar to use with character of this skin color.
    /// </summary>
    public Avatar Avatar
    {
        get { return _avatar; }
    }

    /// <summary>
    /// The body material for this skin color.
    /// </summary>
    public Material BodyMaterial
    {
        get { return _bodyMaterial; }
    }

    /// <summary>
    /// The brows material for this skin color.
    /// </summary>
    public Material BrowsMaterial
    {
        get { return _browsMaterial; }
    }

    /// <summary>
    /// The eye material for this skin color.
    /// </summary>
    public Material EyeMaterial
    {
        get { return _eyeMaterial; }
    }

    /// <summary>
    /// The eye spec material for this skin color.
    /// </summary>
    public Material EyeSpecMaterial
    {
        get { return _eyeSpecMaterial; }
    }
    #endregion
}
