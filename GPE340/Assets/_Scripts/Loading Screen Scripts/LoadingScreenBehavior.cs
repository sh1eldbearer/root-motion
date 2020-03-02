using System.Collections;
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

    [Tooltip("The time, in seconds, the loading screen will take to fade in and out."),
         Space, SerializeField, Range(0.1f, 2f)]
    private float _fadeTime = 1f;
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

        while (timer <= _fadeTime)
        {
            _background.color = new Color(_bgColor.r, _bgColor.b, _bgColor.g, timer / _fadeTime);
            _text.color = new Color(_textColor.r, _textColor.b, _textColor.g, timer / _fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        _isFading = false;
        SceneLoader.sceneLoader.UnloadMainMenu();
    }

    public IEnumerator FadeOut()
    {
        float timer = _fadeTime;
        _isFading = true;

        while (timer >= 0f)
        {
            _background.color = new Color(_bgColor.r, _bgColor.b, _bgColor.g, timer / _fadeTime);
            _text.color = new Color(_textColor.r, _textColor.b, _textColor.g, timer / _fadeTime);
            timer -= Time.deltaTime;
            yield return null;
        }

        _isFading = false;
        SceneLoader.sceneLoader.UnloadLoadingScreen();
    }
}
