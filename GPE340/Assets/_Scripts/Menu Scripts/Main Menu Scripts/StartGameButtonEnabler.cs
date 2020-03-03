using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButtonEnabler : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The Button component for this button."),
        SerializeField] private Button _button;
#pragma warning restore CS0649
    #endregion

    #region Public Properties

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MonitorReadyStatus());
    }

    private IEnumerator MonitorReadyStatus()
    {
        while (true)
        {
            if ((!MainMenuManager.mainMenuMgr.P1ObjectGroup.isActiveAndEnabled || MainMenuManager.mainMenuMgr.P1ObjectGroup.SelectedIndex > 0) &&
                (!MainMenuManager.mainMenuMgr.P2ObjectGroup.isActiveAndEnabled || MainMenuManager.mainMenuMgr.P2ObjectGroup.SelectedIndex > 0) &&
                (!MainMenuManager.mainMenuMgr.P3ObjectGroup.isActiveAndEnabled || MainMenuManager.mainMenuMgr.P3ObjectGroup.SelectedIndex > 0) &&
                (!MainMenuManager.mainMenuMgr.P4ObjectGroup.isActiveAndEnabled || MainMenuManager.mainMenuMgr.P4ObjectGroup.SelectedIndex > 0))
            {
                _button.interactable = true;
            }
            else
            {
                _button.interactable = false;
            }

            yield return null;
        }
    }
}
