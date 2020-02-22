using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager skinMgr;

    public List<ColorData> skinColors;

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
        return skinColors[selectorIndex + 1].color;
    }
}
