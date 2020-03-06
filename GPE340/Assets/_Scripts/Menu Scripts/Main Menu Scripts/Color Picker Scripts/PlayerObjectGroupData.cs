using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class PlayerObjectGroupData : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The player number this color picker is assigned to."),
        SerializeField] private PlayerNumbers _playerNumber;
    [Tooltip("The color selectors that are children of this color picker. " +
         "(REMINDER: A picker's index in this list is 1 less than its index in the ColorNames enum)"), 
        SerializeField]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    private List<ColorSelectorBehavior> _colorSelectors = new List<ColorSelectorBehavior>(8);
    [Tooltip(""),
        SerializeField] private int _selectedIndex = -1;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The player number this color picker is assigned to.
    /// </summary>
    public PlayerNumbers PlayerNumber
    {
        get { return _playerNumber; }
    }

    /// <summary>
    /// The color selectors that are children of this color picker. (REMINDER: A picker's index in this list
    /// is 1 less than its index in the ColorNames enum!)
    /// </summary>
    /// <returns>A list of the color selectors that are children of this color picker.c</returns>
    public List<ColorSelectorBehavior> ColorSelectors
    {
        get { return _colorSelectors; }
    }

    public int SelectedIndex
    {
        get { return _selectedIndex; }
    }
    #endregion

    private void Awake()
    {
#if UNITY_EDITOR
        // Yells at me if I forgot to assign a color picker's player number, but only in the editor
        if (_playerNumber.GetHashCode() == -1)
        {
            Debug.Log($"Color picker {this.gameObject.name} does not have its player number assigned.");
        }

        // Yells at me if I didn't assign all of the color selectors, but only in the editor
        if (_colorSelectors.Count < 8)
        {
            Debug.Log($"You didn't assign all of the color selectors for color picker {this.gameObject.name}.");
        }
#endif

        foreach (ColorSelectorBehavior selector in _colorSelectors)
        {
            selector.SetObjGroupData(this);
        }
    }

    public void SetSelectedIndex(int newIndex)
    {
        if (newIndex >= 0 || newIndex < _colorSelectors.Count)
        {
            _selectedIndex = newIndex;
        }
        else
        {
            throw new ArgumentOutOfRangeException($"The provided index must be between 0 and {_colorSelectors.Count - 1}.");
        }
    }

    public void ClearSelectedIndex()
    {
        _selectedIndex = -1;
    }
}
