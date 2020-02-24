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
    public ColorData[] SkinColors()
    {
        return skinColors;
    }

    /// <summary>
    /// An array of objects containing information about the different skin colors available to players.
    /// </summary>
    /// <param name="index">The index of the specific element you wish to access</param>
    /// <returns></returns>
    public ColorData SkinColors(int index)
    {
        if (index >= 0 && index < skinColors.Length)
        {
            return skinColors[index];
        }
        else
        {
            throw new IndexOutOfRangeException();
        }
    }
    #endregion



    public void Awake()
    {
        if (skinMgr == null)
        {
            skinMgr = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        if (SkinManager.skinMgr == null)
        {
            SkinManager.skinMgr = this;
        }
        else
        {
            Destroy(this);
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
