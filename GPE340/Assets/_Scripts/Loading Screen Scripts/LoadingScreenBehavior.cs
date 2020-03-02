﻿using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenBehavior : MonoBehaviour
{
    public static LoadingScreenBehavior loadingScreen;

    #region Private Properties
#pragma warning disable CS0649
    [Tooltip(""),
        SerializeField] private Image _background;
    private Color _bgColor;
    [Tooltip(""),
        SerializeField] private Text _text;
    private Color _textColor;

    [Tooltip("Denotes if the loading screen is fading in or out."),
        SerializeField] private bool _isFading = true;

#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// Denotes if the loading screen is fading in or out.
    /// </summary>
    public bool IsFading
    {
        get { return _isFading; }
    }
    #endregion

    void Awake()
    {
        loadingScreen = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _bgColor = _background.color;
        _textColor = _text.color;
        //_background.enabled = false;
        //_text.enabled = false;

        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float timer = 0;

        _background.enabled = true;
        _text.enabled = true;
        _isFading = true;

        while (timer <= GameManager.gm.LoadScreenFadeTime)
        {
            _background.color = new Color(_bgColor.r, _bgColor.b, _bgColor.g, timer / GameManager.gm.LoadScreenFadeTime);
            _text.color = new Color(_textColor.r, _textColor.b, _textColor.g, timer / GameManager.gm.LoadScreenFadeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        _isFading = false;
        SceneManager.UnloadSceneAsync(SceneLoader.sceneLoader.MainMenuSceneIndex);
    }

    public IEnumerator FadeOut()
    {
        float timer = GameManager.gm.LoadScreenFadeTime;
        _isFading = true;

        while (timer >= 0f)
        {
            _background.color = new Color(_bgColor.r, _bgColor.b, _bgColor.g, timer / GameManager.gm.LoadScreenFadeTime);
            _text.color = new Color(_textColor.r, _textColor.b, _textColor.g, timer / GameManager.gm.LoadScreenFadeTime);
            timer -= Time.deltaTime;
            yield return null;
        }

        _isFading = false;
    }
}
