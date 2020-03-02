using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm; // Singleton instance for the GameManager

    #region Private Properties
    [Header("Current Game State")]
    [Tooltip("Denotes whether the game is paused or not. Has no effect in the main menu."),
        SerializeField] private bool _isGamePaused;
    [Tooltip("How many players are currently active in the game."),
        SerializeField, Range(0, 4)] private int _totalPlayerCount = 1;
    [Tooltip("The currently active camera rendering the game."), 
        SerializeField] private Camera _currentActiveCamera;

    [Header("Current Player Information")]
    [SerializeField] private PlayerData[] _playerInfo = new PlayerData[4];

    [Header("UI Settings")]
    [Tooltip("The length of time, in seconds, the loading screen will take to fade in and out."),
     Space, SerializeField, Range(0.1f, 2f)] private float _loadScreenFadeTime = 0.5f;
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
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Marks the game as running when I run the main game scene directly from the editor
#if UNITY_EDITOR
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            UnpauseGame();
        }
#endif
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // TODO: Expand functionality to make this setting change between game scenes and non-game scenes
        if (scene.buildIndex == 1)
        {
            _isGamePaused = true;
        }
        else
        {
            _isGamePaused = false;
        }
    }

    public void InitializeGame()
    {

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
}
