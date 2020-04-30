using UnityEngine;
using Utility;

public class StartGameButton : MonoBehaviour
{
    /// <summary>
    /// Method for calling the StartGame function on the SceneLoader.
    /// (Since SceneLoader is a singleton object, but persists from the main menu scene,
    /// this is needed to reference the singleton version of the SceneLoader without breaking
    /// references.)
    /// </summary>
    public void StartGame()
    {
        SceneLoader.sceneLoader.StartGame();
    }
}