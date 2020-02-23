using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm; // Singleton instance for the GameManager

    /* Private Properties */
    [Header("Current Game State")]
    [SerializeField] private bool _isGameRunning;
    [SerializeField, Range(0, 4)] private int _totalPlayerCount = 1;
    [SerializeField] private Camera _currentActiveCamera;

    /* Public Properties */
    public bool IsGameRunning
    {
        get { return _isGameRunning; }
    }

    public int TotalPlayerCount
    {
        get { return _totalPlayerCount; }
    }

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
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            UnpauseGame();
        }
#endif
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // TODO: Expand functionality to make this setting change between game scenes and non-game scenes
        if (scene.name == "MainGame")
        {
            _isGameRunning = true;
        }
        else
        {
            _isGameRunning = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        _isGameRunning = false;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        _isGameRunning = true;
        Time.timeScale = 1;
    }
}
