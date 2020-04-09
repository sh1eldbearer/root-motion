using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class ObjectGroupData : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The player number this color picker is assigned to."),
        SerializeField] private Enums.PlayerNumbers _playerNumber;
    [Tooltip("The color selectors that are children of this color picker. " +
         "(REMINDER: A picker's index in this list is 1 less than its index in the ColorNames enum)"), 
        SerializeField]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    private List<ColorPickerBehavior> _colorPickers = new List<ColorPickerBehavior>(8);
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The player number this color picker is assigned to.
    /// </summary>
    public Enums.PlayerNumbers PlayerNumber
    {
        get { return _playerNumber; }
    }

    /// <summary>
    /// The color selectors that are children of this color picker. (REMINDER: A picker's index in this list
    /// is 1 less than its index in the ColorNames enum!)
    /// </summary>
    /// <returns>A list of the color selectors that are children of this color picker.c</returns>
    public List<ColorPickerBehavior> ColorPickers
    {
        get { return _colorPickers; }
    }
    #endregion

    // Awake is called before Start
    private void Awake()
    {
#if UNITY_EDITOR
        // Yells at me if I forgot to assign a color picker's player number, but only in the editor
        if ((int)_playerNumber == -1)
        {
            Debug.Log($"Color picker {this.gameObject.name} does not have its player number assigned.");
        }

        // Yells at me if I didn't assign all of the color selectors, but only in the editor
        if (_colorPickers.Count < 8)
        {
            Debug.Log($"You didn't assign all of the color selectors for color picker {this.gameObject.name}.");
        }
#endif

        // Tells each color picker which object group it belongs to
        foreach (ColorPickerBehavior picker in _colorPickers)
        {
            picker.SetObjGroupData(this);
        }
    }
}
