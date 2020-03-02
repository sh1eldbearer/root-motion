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

    private Coroutine fadeOut;
#pragma warning restore CS0649
    #endregion

    #region Public Properties

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

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(_mainMenuSceneIndex);
    }

    public void UnloadMainMenu()
    {
        SceneManager.UnloadSceneAsync(_mainMenuSceneIndex);
    }

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

        StartCoroutine(MainMenuManager.mainMenuMgr.LoadingScreen.FadeOut());

        while (LoadingScreenBehavior.loadingScreen.IsFading)
        {
            yield return null;
        }

        UnloadLoadingScreen();
    }
    public void UnloadLoadingScreen()
    {
        SceneManager.UnloadSceneAsync(_loadingScreenIndex);
    }
}
