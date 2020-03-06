using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MenuManager
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip(""),
        SerializeField] private GameObject _pauseMenu;
#pragma warning restore CS0649
    #endregion

    #region Public Properties

    #endregion

    // Start is called before the first frame update
    public override void Start()
    {
        _pauseMenu.SetActive(false);
        StartCoroutine(WaitForMenuCommand());
    }

    private IEnumerator WaitForMenuCommand()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseMenu();
            }

            yield return null;
        }
    }

    public void TogglePauseMenu()
    {
        if (_pauseMenu.activeInHierarchy)
        {
            _pauseMenu.SetActive(false);
            GameManager.gm.UnpauseGame();
        }
        else
        {
            _pauseMenu.SetActive(true);
            GameManager.gm.PauseGame();
        }
    }
}
