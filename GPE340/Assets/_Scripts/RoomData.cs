using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomData : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The spawn point for each player within this room."),
        SerializeField] private Transform _p1SpawnPoint, _p2SpawnPoint, _p3SpawnPoint, _p4SpawnPoint;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The spawn point for player 1.
    /// </summary>
    public Transform P1SpawnPoint
    {
        get { return _p1SpawnPoint; }
    }

    /// <summary>
    /// The spawn point for player 2.
    /// </summary>
    public Transform P2SpawnPoint
    {
        get { return _p2SpawnPoint; }
    }

    /// <summary>
    /// The spawn point for player 3.
    /// </summary>
    public Transform P3SpawnPoint
    {
        get { return _p3SpawnPoint; }
    }

    /// <summary>
    /// The spawn point for player 4.
    /// </summary>
    public Transform P4SpawnPoint
    {
        get { return _p4SpawnPoint; }
    }

    #endregion

    void Awake()
    {
        // TODO: Needs to be removed when multiple rooms come into play, or the game is gonna get VERY confused 
        GameManager.gm.SetCurrentRoomData(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
