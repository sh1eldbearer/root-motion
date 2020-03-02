using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager skinMgr;

    [Tooltip("A array of objects containing information about the different skin colors available to players."), 
        SerializeField] private ColorData[] skinColors = new ColorData[9];

    #region Public Properties
    /// <summary>
    /// An array of objects containing information about the different skin colors available to players.
    /// </summary>
    /// <returns>The array of ColorData objects containing information about the different skin colors available
    /// to players.</returns>
    public ColorData[] SkinColors
    {
        get { return skinColors; }
    }
    #endregion
    

    public void Awake()
    {
        // Singleton pattern
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

    // Update is called once per frame
    public void Update()
    {
        
    }

    public UnityEngine.Color GetRGBColor(int selectorIndex)
    {
        return skinColors[selectorIndex + 1].Color;
    }
}
