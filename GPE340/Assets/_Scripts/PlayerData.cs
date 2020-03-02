using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    #region Private Properties

    [Header("Basic Info")]
    [Tooltip("The player number to assign to this player."),
        SerializeField] private PlayerNumbers _playerNumber;

    [Header("Skin Info")]
    [Tooltip("The character model chosen by this player."),
        SerializeField] private GameObject _playerModel;

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
    /// The character model chosen by this player.
    /// </summary>
    public GameObject PlayerModel
    {
        get { return _playerModel; }
    }

    #endregion

    /// <summary>
    /// Sets the character model for this player.
    /// </summary>
    /// <param name="model">The character model chosen by this player.</param>
    public void SetPlayerModel(GameObject model)
    {
        _playerModel = model;
    }
}
