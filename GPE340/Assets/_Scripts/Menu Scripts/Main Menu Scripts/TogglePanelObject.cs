using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePanelObject : MonoBehaviour
{
#pragma warning disable CS0649
    [Tooltip("The menu panel this button should enable."), 
        SerializeField] private GameObject _panelToEnable;
    [Tooltip("The menu panel this button should disable."), 
        SerializeField] private List<GameObject> _panelsToDisable;
#pragma warning restore CS0649

    /// <summary>
    /// Toggles the panel as visible or invisible
    /// </summary>
    public void TogglePanel()
    {
        _panelToEnable.SetActive(true);
        foreach (GameObject panel in _panelsToDisable)
        {
            panel.SetActive(false);
        }
    }
}
