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
    [Tooltip("The camera rendering the game."), 
        SerializeField] private Camera _gameCamera;
    [Tooltip("The game camera's CameraController component."),
        SerializeField] private CameraController _gameCameraController;
    [Tooltip("When in the game proper, this is the room the players are currently in."),
        SerializeField, Space] private RoomData _currentRoomData;

    [Header("Current Player Information")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local"), 
        SerializeField] private PlayerTracking[] _playerInfo;

    // [Header("Prefabs")]

    [Header("Game Settings")]
    [Tooltip("The amount of health a player pawn starts with."),
        SerializeField] private float _initialPlayerHealth = 100f;
    [Tooltip("The length of time, in seconds, it takes for a player to respawn after death."),
        SerializeField] private float _playerRespawnTimer = 5f;

    [Tooltip("The amount of health an enemy pawn starts with."),
        Space(5f), SerializeField] private float _initialEnemyHealth = 50f;
    
    

    [Header("UI Settings")]
    [Tooltip("The length of time, in seconds, the loading screen will take to fade in and out."),
        Space, SerializeField, Range(0.1f, 2f)] private float _loadScreenFadeTime = 0.5f;
    [Tooltip("The time, in seconds, that pawn health UIs will take to update their slider values."),
        SerializeField, Range(0.01f, 1f)] private float _healthSliderUpdateTime = 0.15f;
    [Tooltip("The color health bars should display when they are at full health."),
        SerializeField] private Color _fullHealthColor;
    [Tooltip("The color health bars should display when they are low or zero health."),
        SerializeField] private Color _zeroHealthColor;
#pragma warning restore CS0649
    #endregion

    #region Public Properties

    /// <summary>
    /// The camera rendering the game.
    /// </summary>
    public Camera GameCamera
    {
        get { return _gameCamera; }
    }

    /// <summary>
    /// The game camera's CameraController component.
    /// </summary>
    public CameraController GameCameraController
    {
        get { return _gameCameraController; }
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
    public PlayerTracking[] PlayerInfo
    {
        get { return _playerInfo; }
    }

    /// <summary>
    /// The amount of health a player pawn starts with.
    /// </summary>
    public float InitialPlayerHealth
    {
        get { return _initialPlayerHealth; }
    }

    /// <summary>
    /// The length of time, in seconds, it takes for a player to respawn after death.
    /// </summary>
    public float PlayerRespawnTimer
    {
        get { return _playerRespawnTimer; }
    }

    /// <summary>
    /// The amount of health an enemy pawn starts with.
    /// </summary>
    public float InitialEnemyHealth
    {
        get { return _initialEnemyHealth; }
    }

    /// <summary>
    /// The length of time, in seconds, the loading screen will take to fade in and out.
    /// </summary>
    public float LoadScreenFadeTime
    {
        get { return _loadScreenFadeTime; }
    }

    /// <summary>
    /// The time, in seconds, that pawn health UIs will take to update their slider values.
    /// </summary>
    public float HealthSliderUpdateTime
    {
        get { return _healthSliderUpdateTime; }
    }

    /// <summary>
    /// The color health bars should display when they are at full health.
    /// </summary>
    public Color FullHealthColor
    {
        get { return _fullHealthColor; }
    }

    /// <summary>
    /// The color health bars should display when they are low or zero health.
    /// </summary>
    public Color ZeroHealthColor
    {
        get { return _zeroHealthColor; }
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
#if UNITY_EDITOR
        // Marks the game as running when I run the main game scene directly from the editor
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PauseManager.pauseMgr.UnpauseGame();
        }
#endif

        ResetPlayerInfo();
    }

    /// <summary>
    /// Sets the active camera for the game scene.
    /// </summary>
    /// <param name="gameCamera"></param>
    public void SetGameCamera(Camera gameCamera, CameraController cameraController)
    {
        _gameCamera = gameCamera;
        _gameCameraController = cameraController;
    }

    /// <summary>
    /// Resets the player info to the default state
    /// </summary>
    public void ResetPlayerInfo()
    {
        foreach (PlayerTracking player in PlayerInfo)
        {
            player.ResetPlayerInfo();
        }
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
