using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /* Static Properties */
    public static GameManager gm; // Singleton instance for the GameManager
    public static CameraController cam; // Instance for the currently active camera
    public static SkinManager skinMgr;
    public static MenuManager menuMgr; // Holds a reference to the current scene's menu manager

    /* Inspector Properties */
    [Header("Current Game State")]
    [SerializeField] private bool isGameRunning;
    [SerializeField] private Camera currentActiveCamera;

    /* Public Properties */
    public bool IsGameRunning
    {
        get { return isGameRunning; }
    }

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

        skinMgr = this.gameObject.GetComponent<SkinManager>();
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
            isGameRunning = true;
        }
        else
        {
            isGameRunning = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        isGameRunning = false;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        isGameRunning = true;
        Time.timeScale = 1;
    }
}
