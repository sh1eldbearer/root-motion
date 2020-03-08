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
        SerializeField] private bool _isGameRunning;
    [Tooltip("The currently active camera rendering the game."), 
        SerializeField] private Camera _activeCamera;
    [Tooltip("When in the game proper, this is the room the players are currently in."),
        SerializeField, Space] private RoomData _currentRoomData;

    [Header("Current Player Information")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local"), 
        SerializeField] private PlayerData[] _playerInfo = new PlayerData[4];

    // [Header("Prefabs")]

    [Header("UI Settings")]
    [Tooltip("The length of time, in seconds, the loading screen will take to fade in and out."),
     Space, SerializeField, Range(0.1f, 2f)] private float _loadScreenFadeTime = 0.5f;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// Denotes whether the game is paused or not. Has no effect in the main menu.
    /// </summary>
    public bool IsGameRunning
    {
        get { return _isGameRunning; }
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
    /// The length of time, in seconds, the loading screen will take to fade in and out.
    /// </summary>
    public float LoadScreenFadeTime
    {
        get { return _loadScreenFadeTime; }
    }

    #endregion

    // Awake is called before Start
    private void Awake()
    {
        // Makes the GameManager a singleton and a persistent game object
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
    private void Start()
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
        _isGameRunning = false;
        Time.timeScale = 0;
    }

    /// <summary>
    /// Unpauses the game.
    /// </summary>
    public void UnpauseGame()
    {
        _isGameRunning = true;
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
