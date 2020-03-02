using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenFader : MonoBehaviour
{
    public static LoadingScreenFader loadScreenFader;

    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The background image for the loading screen. Needed to access the Color component of " +
             "the image."),
        SerializeField] private Image _background;
    private Color _bgColor;
    [Tooltip("The text object for the loading screen. Needed to access the Color component of " +
             "the text."),
        SerializeField] private Text _text;
    private Color _textColor;

    [Tooltip("Denotes if the loading screen is fading in or out. Can be read to tell scene loading" +
             "coroutines to wait before proceeding to their next step."),
        Space, SerializeField] private bool _isFading = true;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// Denotes if the loading screen is fading in or out. Can be read to tell scene loading coroutines
    /// to wait before proceeding to their next step.
    /// </summary>
    public bool IsFading
    {
        get { return _isFading; }
    }
    #endregion

    void Awake()
    {
        // Assigns this script as a globally accessible object
        loadScreenFader = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Gets the color components of the UI elements so the alpha can be adjusted
        _bgColor = _background.color;
        _textColor = _text.color;

        StartCoroutine(FadeIn());
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
        float timer = 0;

        _isFading = true;

        // Slowly fades the UI's alphas from 0 up to 100 (fully invisible to fully opaque)
        while (timer <= GameManager.gm.LoadScreenFadeTime)
        {
            _background.color = new Color(_bgColor.r, _bgColor.b, _bgColor.g, timer / GameManager.gm.LoadScreenFadeTime);
            _text.color = new Color(_textColor.r, _textColor.b, _textColor.g, timer / GameManager.gm.LoadScreenFadeTime);
            timer += Time.deltaTime;
            yield return null;
        }

        _isFading = false;
    }

    /// <summary>
    /// Decreases the alpha the UI elements of the loading screen until they are hidden from view in the
    /// canvas.
    /// </summary>
    /// <returns>Null.</returns>
    public IEnumerator FadeOut()
    {
        float timer = GameManager.gm.LoadScreenFadeTime;

        _isFading = true;

        // Slowly fades the UI's alphas from 100 down to 0 (fully opaque to fully invisible)
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
