using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Loads a scene based on the scene's name.
    /// </summary>
    /// <param name="sceneName">The name of the scene to be loaded.</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Loads a scene based on the scene's number in the build order.
    /// </summary>
    /// <param name="sceneName">The build order number of the scene to be loaded.</param>
    public void LoadScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
