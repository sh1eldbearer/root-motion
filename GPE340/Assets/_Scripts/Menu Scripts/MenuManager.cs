using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class MenuManager : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The loading screen assigned to this scene."),
        SerializeField]private LoadingScreenFader _loadingScreen;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The loading screen assigned to this scene.
    /// </summary>
    public LoadingScreenFader LoadingScreen
    {
        get { return _loadingScreen; }
    }
    #endregion

    // Start is called before the first frame update
    public virtual void Start()
    {

    }
}
