using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader sceneLoader;

    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The build order index of the main menu scene."),
        SerializeField] private int _mainMenuSceneIndex = 0;
    [Tooltip("The build order index of the main game scene."),
        SerializeField] private int _gameSceneIndex = 1;
    [Tooltip("The build order index of the loading screen scene."),
        SerializeField] private int _loadingScreenIndex = 2;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The build order index of the main menu scene.
    /// </summary>
    public int MainMenuSceneIndex
    {
        get { return _mainMenuSceneIndex; }
    }

    /// <summary>
    /// The build order index of the main game scene.
    /// </summary>
    public int GameSceneIndex
    {
        get { return _gameSceneIndex; }
    }

    /// <summary>
    /// The build order index of the loading screen scene.
    /// </summary>
    public int LoadingScreenIndex
    {
        get { return _loadingScreenIndex; }
    }
    #endregion

    private void Awake()
    {
        // Makes the scene loader a singleton and a persistent game object
        if (sceneLoader == null)
        {
            sceneLoader = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Starts the game from the main menu.
    /// </summary>
    public void StartGame()
    {
        StartCoroutine(SceneLoader.sceneLoader.LoadGameScene());
    }

    /// <summary>
    /// Performs all necessary operations to transition from the main menu, to the loading screen, to the
    /// main game scene.
    /// </summary>
    /// <returns>Null.</returns>
    private IEnumerator LoadGameScene()
    {
        // Loads the loading screen scene additively, and waits for the loading screen UI to finish fading in before proceeding
        SceneManager.LoadScene(_loadingScreenIndex, LoadSceneMode.Additive);
        while (LoadingScreenFader.loadScreenFader == null || LoadingScreenFader.loadScreenFader.IsFading)
        {
            yield return null;
        }

        // Unloads the main menu scene
        SceneManager.UnloadSceneAsync(_mainMenuSceneIndex);

        // Loads the main game scene additively, and waits for the scene to finish loading before proceeding
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(_gameSceneIndex, LoadSceneMode.Additive);
        while (!sceneLoad.isDone)
        {
            yield return null;
        }
        
        // Waits a single frame, then sets the main game scene as the active scene to make its lighting the dominant scene lighting
        yield return 0;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_gameSceneIndex));

        // Starts fading the loading screen UI out, and waits for it to finish before proceeding
        StartCoroutine(LoadingScreenFader.loadScreenFader.FadeOut());
        while (LoadingScreenFader.loadScreenFader.IsFading)
        {
            yield return null;
        }

        // Unloads the loading screen scene
        SceneManager.UnloadSceneAsync(_loadingScreenIndex);
    }
}
