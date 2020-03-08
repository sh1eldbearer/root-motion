using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using TMPro.EditorUtilities;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader sceneLoader;  // Singleton instance for the SceneLoader

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

    // Awake is called before Start
    private void Awake()
    {
        // Makes the SceneLoader a singleton and a persistent game object
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
        StartCoroutine(LoadScene(_mainMenuSceneIndex, _gameSceneIndex));
    }

    /// <summary>
    /// Goes back to the main menu from the game scene.
    /// </summary>
    public void LoadMainMenu()
    {
        StartCoroutine(LoadScene(_gameSceneIndex, _mainMenuSceneIndex));
    }

    /// <summary>
    /// Performs all necessary operations to transition from between two scenes, while displaying the loading
    /// screen between scenes.
    /// </summary>
    /// <param name="originScene">The scene the game is currently in, and that will be unloaded.</param>
    /// <param name="targetScene">The scene the game will load.</param>
    /// <returns>Null.</returns>
    private IEnumerator LoadScene(int originScene, int targetScene)
    {
        // Loads the loading screen scene additively, and waits for the loading screen UI to finish fading in before proceeding
        SceneManager.LoadSceneAsync(_loadingScreenIndex, LoadSceneMode.Additive);

        // Waits for the loading screen fade in to finish
        while (LoadingScreenFader.loadScreenFader == null || LoadingScreenFader.loadScreenFader.IsFading)
        {
            yield return null;
        }

        // Unloads the main menu scene
        SceneManager.UnloadSceneAsync(originScene);

        // Loads the main game scene additively, and waits for the scene to finish loading before proceeding
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        while (!sceneLoad.isDone)
        {
            yield return null;
        }
        
        // Waits a single frame, then sets the new scene as the active scene to make its lighting the dominant scene lighting
        yield return 0;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(targetScene));

        // Starts fading the loading screen UI out, and waits for it to finish before proceeding
            yield return StartCoroutine(LoadingScreenFader.loadScreenFader.FadeOut());

        // Unloads the loading screen scene
        SceneManager.UnloadSceneAsync(_loadingScreenIndex);
        yield return true;
    }
}
