using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Basic Info")]
    [Tooltip("The player number to assign to this player."),
        SerializeField] private PlayerNumbers _playerNumber;
    [Tooltip("The player's current status."),
        SerializeField] private PlayerStatus _playerStatus;

    [Header("Skin Info")]
    [Tooltip("The index of the character model chosen by this player."),
        SerializeField] private int _skinColorIndex;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The player number to assigned to this player.
    /// </summary>
    public PlayerNumbers PlayerNumber
    {
        get { return _playerNumber; }
    }

    /// <summary>
    /// The player's current status.
    /// </summary>
    public PlayerStatus Status
    {
        get { return _playerStatus; }
    }

    /// <summary>
    /// The index of the character model chosen by this player.
    /// </summary>
    public int SkinColorIndex
    {
        get { return _skinColorIndex; }
    }

    #endregion

    /// <summary>
    /// Stores the index of the skin color chosen by this player, and marks the player
    /// as Ready.
    /// </summary>
    /// <param name="model">The index of the skin color chosen by this player.</param>
    public void SetSkinColorIndex(int index)
    {
        _skinColorIndex = index;
        _playerStatus = PlayerStatus.Ready;
    }

    /// <summary>
    /// Clears the index of the skin color for this player and removes the player's
    /// Ready status.
    /// from the player's info.
    /// </summary>
    public void ClearSkinColorIndex()
    {
        _skinColorIndex = 0;
        _playerStatus = PlayerStatus.Joined;
    }
}
