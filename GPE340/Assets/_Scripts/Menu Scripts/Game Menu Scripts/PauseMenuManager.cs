using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MenuManager
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The pause menu GameObject. (Used to enable/disable the object.)"),
        SerializeField] private GameObject _pauseMenu;
#pragma warning restore CS0649
    #endregion

    // Start is called before the first frame update
    protected override void Start()
    {
        // Hides the pause menu on scene start
        _pauseMenu.SetActive(false);
        // Waits for the keypress to trigger the pause menu
        StartCoroutine(WaitForMenuKey());
    }

    /// <summary>
    /// Waits for the keypress to trigger the pause menu.
    /// </summary>
    /// <returns>Null.</returns>
    private IEnumerator WaitForMenuKey()
    {
        // TODO: Could be rebuilt as an event for cleaning debugging?

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseMenu();
            }

            yield return null;
        }
    }

    /// <summary>
    /// Enables and disables the pause menu, and pauses and unpauses the game.
    /// </summary>
    public void TogglePauseMenu()
    {
        if (_pauseMenu.activeInHierarchy)   // Pause menu is open
        {
            // Close menu and unpause game
            _pauseMenu.SetActive(false);
            GameManager.gm.UnpauseGame();
        }
        else                                // Pause menu is closed
        {
            // Open menu and pause game
            _pauseMenu.SetActive(true);
            GameManager.gm.PauseGame();
        }
    }
}
