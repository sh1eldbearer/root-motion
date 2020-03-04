using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm; // Singleton instance for the GameManager

    #region Private Properties
#pragma warning disable CS0649
    [Header("Current Game State")]
    [Tooltip("Denotes whether the game is paused or not. Has no effect in the main menu."),
        SerializeField] private bool _isGamePaused;
    [Tooltip("How many players are currently active in the game."),
        SerializeField, Range(0, 4)] private int _totalPlayerCount = 1;
    [Tooltip("The currently active camera rendering the game."), 
        SerializeField] private Camera _currentActiveCamera;
    [Tooltip("When in the game proper, this is the room the players are currently in."),
        SerializeField, Space] private RoomData _currentRoomData;

    [Header("Current Player Information")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local"), 
        SerializeField] private PlayerData[] _playerInfo = new PlayerData[4];

    [Header("Prefabs")]
    [Tooltip("The prefab object for the player object that contains all the scripts for the players."),
        SerializeField]private GameObject _playerPrefab;

    [Header("UI Settings")]
    [Tooltip("The length of time, in seconds, the loading screen will take to fade in and out."),
     Space, SerializeField, Range(0.1f, 2f)] private float _loadScreenFadeTime = 0.5f;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// Denotes whether the game is paused or not. Has no effect in the main menu.
    /// </summary>
    public bool IsGamePaused
    {
        get { return _isGamePaused; }
    }

    /// <summary>
    /// How many players are currently active in the game.
    /// </summary>
    public int TotalPlayerCount
    {
        get { return _totalPlayerCount; }
    }

    /// <summary>
    /// The RoomData component of the room the players are currently occupying.
    /// </summary>
    public RoomData CurrentRoomData
    {
        get { return _currentRoomData; }
    }

    /// <summary>
    /// Current information about the players.
    /// </summary>
    public PlayerData[] PlayerInfo
    {
        get { return _playerInfo; }
    }

    /// <summary>
    /// The prefab object for spawning players.
    /// </summary>
    public GameObject PlayerPrefab
    {
        get { return _playerPrefab; }
    }

    /// <summary>
    /// The length of time, in seconds, the loading screen will take to fade in and out.
    /// </summary>
    public float LoadScreenFadeTime
    {
        get { return _loadScreenFadeTime; }
    }

    #endregion

    void Awake()
    {
        // GameManager exists as a singleton object
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Marks the game as running when I run the main game scene directly from the editor
#if UNITY_EDITOR
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            UnpauseGame();
        }
#endif
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame()
    {
        _isGamePaused = false;
        Time.timeScale = 0;
    }

    /// <summary>
    /// Unpauses the game.
    /// </summary>
    public void UnpauseGame()
    {
        _isGamePaused = true;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Changes the current RoomData component.
    /// </summary>
    /// <param name="roomData">The RoomData component of the room the room the players are
    /// currently occupying.</param>
    public void SetCurrentRoomData(RoomData roomData)
    {
        _currentRoomData = roomData;
    }
}
