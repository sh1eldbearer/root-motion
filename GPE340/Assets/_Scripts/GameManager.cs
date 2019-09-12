using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RaycastingMouse))]
public class GameManager : MonoBehaviour
{
    public static GameManager theGM;

    [Header("Game Components")]
    public RaycastingMouse mouse;

    public bool gameIsRunning;


    void Awake()
    {
        // Singleton pattern
        if (theGM == null)
        {
            theGM = this;
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
        if (scene.name == "MainGame")
        {
            gameIsRunning = true;
            mouse.StartCoroutines();
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
