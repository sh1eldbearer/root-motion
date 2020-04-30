﻿using System.Collections;
using UnityEngine;

public class LoadingScreenFader : MonoBehaviour
{
    public static LoadingScreenFader loadScreenFader; // Singleton instance for the LoadingScreen

    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The Canvas Group component for the loading screen UI elements."),
        SerializeField] private CanvasGroup _canvasGroup;
    [Tooltip("The camera used only in the loading screen. (Used to enable this camera just before unloading the previous scene."),
        SerializeField] private Camera _loadingScreenCam;
#pragma warning restore CS0649
    #endregion

    // Awake is called before Start
    private void Awake()
    {
        // Assigns this script as a globally accessible object
        loadScreenFader = this;

        // Component reference assignments
        if (_canvasGroup == null)
        {
            _canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        }
    }

    void OnDestroy()
    {
        loadScreenFader = null;
    }

    /// <summary>
    /// Increases the alpha the UI elements of the loading screen until they obscure the view of the
    /// canvas.
    /// </summary>
    /// <returns>Null.</returns>
    public IEnumerator FadeIn()
    {
        float timer = 0f;

        // Fades the UI's alphas from 0 up to 100 (fully invisible to fully opaque)
        while (timer <= GameManager.gm.LoadScreenFadeTime)
        {
            _canvasGroup.alpha = timer / GameManager.gm.LoadScreenFadeTime;
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Enables the camera on the loading screen
        _loadingScreenCam.enabled = true;
    }

    /// <summary>
    /// Decreases the alpha the UI elements of the loading screen until they are hidden from view in the
    /// canvas.
    /// </summary>
    /// <returns>Null.</returns>
    public IEnumerator FadeOut()
    {
        float timer = GameManager.gm.LoadScreenFadeTime;

        // Fades the UI's alphas from 100 down to 0 (fully opaque to fully invisible)
        while (timer >= 0f)
        {
            _canvasGroup.alpha = timer / GameManager.gm.LoadScreenFadeTime;
            timer -= Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
