using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    private PlayerNumbers _playerNumber;

    public PlayerNumbers PlayerNumber
    {
        get { return _playerNumber; }
    }
}
