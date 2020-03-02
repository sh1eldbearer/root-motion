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
    /// Performs all necessary
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadGameScene()
    {
        SceneManager.LoadScene(_loadingScreenIndex, LoadSceneMode.Additive);

        while (LoadingScreenBehavior.loadingScreen == null || LoadingScreenBehavior.loadingScreen.IsFading)
        {
            yield return null;
        }

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(_gameSceneIndex, LoadSceneMode.Additive);

        while (!sceneLoad.isDone)
        {
            yield return null;
        }


        yield return 0;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_gameSceneIndex));

        StartCoroutine(LoadingScreenBehavior.loadingScreen.FadeOut());

        while (LoadingScreenBehavior.loadingScreen.IsFading)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(_loadingScreenIndex);
    }
}
