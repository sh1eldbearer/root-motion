using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    [Header("Current Game State")]
    public bool gameIsRunning;

    //[Header("Game Components")]


    void Awake()
    {
        // Singleton pattern
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
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // TODO: Expand functionality to make this setting change between game scenes and non-game scenes
        if (scene.name == "MainGame")
        {
            gameIsRunning = true;
        }
        else
        {
            gameIsRunning = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
