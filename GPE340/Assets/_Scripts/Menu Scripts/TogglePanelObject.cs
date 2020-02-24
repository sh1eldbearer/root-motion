using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanelObject : MonoBehaviour
{
    [Tooltip("The menu panel this button should enable."), 
        SerializeField] private GameObject panelToEnable;
    [Tooltip("The menu panel this button should disable."), 
        SerializeField] private List<GameObject> panelsToDisable;

    /// <summary>
    /// Toggles the panel as visible or invisible
    /// </summary>
    public void TogglePanel()
    {
        panelToEnable.SetActive(true);
        foreach (GameObject panel in panelsToDisable)
        {
            panel.SetActive(false);
        }
    }
}
