using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Designates which player an object is assigned to. Each player value has a corresponding integer value.
/// </summary>
public enum PlayerNumber
{
    Unassigned = 0,
    P1 = 1,
    P2 = 2,
    P3 = 3,
    P4 = 4
}

public class PlayerObjectGroupData : MonoBehaviour
{
    /* Private Properties */
    [Tooltip("The player number this color picker is assigned to"),
        SerializeField] private PlayerNumber _playerNumber;

    /* Public Properties */
    public PlayerNumber PlayerNumberEnum // Returns the player number as an enumerator value
    {
        get { return _playerNumber; }
    }

    public int PlayerNumber // Returns the player number as the enumerator's corresponding integer value
    {
        get { return (int)(Convert.ChangeType(_playerNumber, _playerNumber.GetTypeCode())); }
    }

    [Tooltip("The color selectors that are children of this color picker. " +
             "(REMINDER: A picker's index in this list is 1 less than its index in the ColorNames enum)")]
    public List<GameObject> colorPickers;

    public void Awake()
    {
#if UNITY_EDITOR
        // Yells at me if I forgot to assign a color picker's player number, but only in the editor
        if (_playerNumber == 0)
        {
            Debug.Log($"Color picker {this.gameObject.name} does not have its player number assigned.");
        }

        // Yells at me if I didn't assign all of the color selectors, but only in the editor
        if (colorPickers.Count < 8)
        {
            Debug.Log($"You didn't assign all of the color selectors for color picker {this.gameObject.name}.");
        }
#endif 
    }
}
