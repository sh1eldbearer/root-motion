using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Tooltip("The name of the game's menu scene"), 
        SerializeField] private string menuSceneName = "Menu";
    [Tooltip("The name of the game's main scene"), 
        SerializeField] public string mainGameSceneName = "MainGame";
    
    /// <summary>
    /// 
    /// </summary>
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    /// <summary>
    /// Loads the main game scene, based on the scene name set in the inspector
    /// </summary>
    public void LoadMainGameScene()
    {
        SceneManager.LoadScene(mainGameSceneName);
    }
}
