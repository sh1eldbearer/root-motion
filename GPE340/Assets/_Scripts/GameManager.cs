﻿using System;
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
    [SerializeField] private bool _isGameRunning;
    [SerializeField, Range(0, 4)] private int _totalPlayerCount = 1;
    [Tooltip("The currently active camera rendering the game."), 
        SerializeField] private Camera _currentActiveCamera;

    [Header("Current Player Information")]
    [SerializeField] private PlayerData[] _playerInfo = new PlayerData[4];
    #endregion

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
