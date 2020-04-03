using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    /// <summary>
    /// Method for calling the LoadMainMenu function on the SceneLoader.
    /// (Since SceneLoader is a singleton object, but persists from the main menu scene,
    /// this is needed to reference the singleton version of the SceneLoader without breaking
    /// any references.)
    /// </summary>
    public void ExitToMainMenu()
    {
        SceneLoader.sceneLoader.LoadMainMenu();
    }
}
