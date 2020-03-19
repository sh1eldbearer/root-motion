using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager skinMgr;  // Singleton instance for the SkinManager

    [Tooltip("A array of objects containing information about the different skin colors available to players."), 
        SerializeField] private SkinColorData[] _skinColors = new SkinColorData[9];

    #region Public Properties
    /// <summary>
    /// An array of objects containing information about the different skin colors available to players.
    /// </summary>
    /// <returns>The array of ColorData objects containing information about the different skin colors available
    /// to players.</returns>
    public SkinColorData[] SkinColors
    {
        get { return _skinColors; }
    }
    #endregion

    // Awake is called before Start
    private void Awake()
    {
        /* Makes the SkinManager a singleton but not a persistent game object
         (Once the game is running, the configuration of the SkinManager will be stored
         in the static class member, and will no longer need to be viewable or editable) */
        if (skinMgr == null)
        {
            skinMgr = this;
        }
        Destroy(this.gameObject);
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
