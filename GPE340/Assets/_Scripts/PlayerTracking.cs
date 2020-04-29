using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utility.Enums;

[System.Serializable]
public class PlayerTracking
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Basic Info")]
    [Tooltip("The player number to assign to this player."),
        SerializeField] private PlayerNumbers _playerNumber;
    [Tooltip("The player's current status."),
        SerializeField] private PlayerStatus _playerStatus;
    [Tooltip("The agent data component of this player (if active.)"),
        SerializeField] private PawnData _pawnData;

    [Header("Skin Info")]
    [Tooltip("The index of the character model chosen by this player."),
        SerializeField] private int _skinColorIndex = -1;
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
    /// The agent data component of this player (if active.)
    /// </summary>
    public PawnData PawnData
    {
        get { return _pawnData; }
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
    /// Resets some of the properties of this PlayerData object.
    /// </summary>
    public void ResetPlayerInfo()
    {
        // Player 1 should always be at least joined
        if (_playerNumber == PlayerNumbers.P1)
        {
            _playerStatus = PlayerStatus.Joined;
        }
        else
        {
            // Players 2-4 should be "not joined" if they were active in a previous game this session
            if ((int)_playerStatus >= 0)
            {
                _playerStatus = PlayerStatus.NotJoined;
            }
            // Players 2-4 should be "inactive" if they were not active in any previous game this session
            else
            {
                _playerStatus = PlayerStatus.Inactive;
            }
        }

        // Clears other information about the players
        ClearAgentData();
        ClearSkinColorIndex();
    }

    /// <summary>
    /// Sets this player's status.
    /// </summary>
    public void SetStatus(PlayerStatus newStatus)
    {
        _playerStatus = newStatus;
    }

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
    /// </summary>
    public void ClearSkinColorIndex()
    {
        _skinColorIndex = -1;

        if ((int) _playerStatus >= 0)
        {
            _playerStatus = PlayerStatus.Joined;
        }
    }

    /// <summary>
    /// Assigns a reference to this player's agent data when the game is running. 
    /// </summary>
    /// <param name="pawnData">The agent data to be associated with this player.</param>
    public void SetPawnData(PawnData pawnData)
    {
        _pawnData = pawnData;
    }

    /// <summary>
    /// Clears the reference to this player's agent data.
    /// </summary>
    public void ClearAgentData()
    {
        _pawnData = null;
    }
}
