using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager skinMgr;  // Singleton instance for the SkinManager

    [Tooltip("A array of objects containing information about the different skin colors available to players."), 
        SerializeField] private SkinColorDataStruct[] _skinColors = new SkinColorDataStruct[9];

    #region Public Properties
    /// <summary>
    /// An array of objects containing information about the different skin colors available to players.
    /// </summary>
    /// <returns>The array of ColorData objects containing information about the different skin colors available
    /// to players.</returns>
    public SkinColorDataStruct[] SkinColors
    {
        get { return _skinColors; }
    }
    #endregion

    // Awake is called before Start
    private void Awake()
    {
        // Makes the SkinManager a singleton and a persistent game object
        if (skinMgr == null)
        {
            skinMgr = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Gets the color value set for this skin color (used for UI displays).
    /// </summary>
    /// <param name="selectorIndex"></param>
    /// <returns>A color object representing the color assigned this skin color.</returns>
    public UnityEngine.Color GetRGBColor(int selectorIndex)
    {
        return _skinColors[selectorIndex + 1].Color;
    }
}
