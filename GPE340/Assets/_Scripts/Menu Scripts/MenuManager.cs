using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Start()
    {
        if (GameManager.menuMgr != this)
        {
            GameManager.menuMgr = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
