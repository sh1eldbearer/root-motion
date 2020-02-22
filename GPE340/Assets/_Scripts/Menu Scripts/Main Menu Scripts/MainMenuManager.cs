using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MenuManager
{
    public static MainMenuManager mainMenuMgr;

    [Header("Panel Objects")]
    [Tooltip("The GameObject containing the panel for the main menu's new game menu."),
        SerializeField] private GameObject _newGameMenu;
    [Tooltip("The GameObject containing the panel for the main menu's options menu."), 
        SerializeField] private GameObject _optionsMenu;

    [Header("Player Object Groups")]
    [SerializeField] private PlayerObjectGroupData _p1ObjectGroup;
    [SerializeField] private PlayerObjectGroupData _p2ObjectGroup;
    [SerializeField] private PlayerObjectGroupData _p3ObjectGroup;
    [SerializeField] private PlayerObjectGroupData _p4ObjectGroup;

    [Header("Player 1 Objects")]
    [SerializeField] private GameObject _p1JoinLabel;
    [SerializeField] private GameObject _p1ColorPicker;
    [SerializeField] private GameObject _p1ReadyLabel;
    
    [Header("Player 2 Objects")]
    [SerializeField] private GameObject _p2JoinLabel;
    [SerializeField] private GameObject _p2ColorPicker;
    [SerializeField] private GameObject _p2ReadyLabel;

    [Header("Player 3 Objects")]
    [SerializeField] private GameObject _p3JoinLabel;
    [SerializeField] private GameObject _p3ColorPicker;
    [SerializeField] private GameObject _p3ReadyLabel;

    [Header("Player 4 Objects")]
    [SerializeField] private GameObject _p4JoinLabel;
    [SerializeField] private GameObject _p4ColorPicker;
    [SerializeField] private GameObject _p4ReadyLabel;

    #region Public Properties
    public PlayerObjectGroupData P1ObjectGroup
    {
        get { return _p1ObjectGroup; }
    }
    public PlayerObjectGroupData P2ObjectGroup
    {
        get { return _p2ObjectGroup; }
    }
    public PlayerObjectGroupData P3ObjectGroup
    {
        get { return _p3ObjectGroup; }
    }
    public PlayerObjectGroupData P4ObjectGroup
    {
        get { return _p4ObjectGroup; }
    }

    public GameObject P1JoinLabel
    {
        get { return _p1JoinLabel; }
    }
    public GameObject P1ColorPicker
    {
        get { return _p1ColorPicker; }
    }
    public GameObject P1ReadyLabel
    {
        get { return _p1ReadyLabel; }
    }

    public GameObject P2JoinLabel
    {
        get { return _p2JoinLabel; }
    }
    public GameObject P2ColorPicker
    {
        get { return _p2ColorPicker; }
    }
    public GameObject P2ReadyLabel
    {
        get { return _p2ReadyLabel; }
    }

    public GameObject P3JoinLabel
    {
        get { return _p3JoinLabel; }
    }
    public GameObject P3ColorPicker
    {
        get { return _p3ColorPicker; }
    }
    public GameObject P3ReadyLabel
    {
        get { return _p3ReadyLabel; }
    }

    public GameObject P4JoinLabel
    {
        get { return _p4JoinLabel; }
    }
    public GameObject P4ColorPicker
    {
        get { return _p4ColorPicker; }
    }
    public GameObject P4ReadyLabel
    {
        get { return _p4ReadyLabel; }
    }
    
    #endregion

    public void Awake()
    {
        if (mainMenuMgr == null)
        {
            mainMenuMgr = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public override void Start()
    {
        // TODO: Properly configure this
        switch (GameManager.gm.TotalPlayerCount)
        {
            case 0:
                break;
            case 1:
                ShowColorPicker(PlayerNumber.P1);
                ShowColorPicker(PlayerNumber.P2);
                ShowColorPicker(PlayerNumber.P3);
                ShowColorPicker(PlayerNumber.P4);
                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
        }
    }

    /// <summary>
    /// Registers a UI element with this MainMenuManager.
    /// </summary>
    /// <param name="uiElement">The UI element to be registered.</param>
    /// <param name="playerNumber">The player number (determined by the PlayerNumber enumerator).</param>
    /// <param name="elementType">The type of element being registered.</param>
    public void RegisterUIElement(GameObject uiElement, int playerNumber, NewGameUIElementType elementType)
    {
        switch (elementType)
        {
            case NewGameUIElementType.ObjectGroup:
                RegisterObjectGroup(uiElement.GetComponent<PlayerObjectGroupData>(), playerNumber);
                break;
            case NewGameUIElementType.JoinLabel:
                RegisterJoinLabel(uiElement, playerNumber);
                break;
            case NewGameUIElementType.ColorPicker:
                RegisterColorPicker(uiElement, playerNumber);
                break;
            case NewGameUIElementType.ReadyLabel:
                RegisterReadyLabel(uiElement, playerNumber);
                break;
        }
    }

    /// <summary>
    /// Registers an object group with this MainMenuManager.
    /// </summary>
    /// <param name="objGroupData">The UI element to be registered.</param>
    /// <param name="playerNumber">The player number (determined by the PlayerNumber enumerator).</param>
    private void RegisterObjectGroup(PlayerObjectGroupData objGroupData, int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                _p1ObjectGroup = objGroupData;
                break;
            case 2:
                _p2ObjectGroup = objGroupData;
                break;
            case 3:
                _p3ObjectGroup = objGroupData;
                break;
            case 4:
                _p4ObjectGroup = objGroupData;
                break;
        }
    }

    /// <summary>
    /// Registers a join label with this MainMenuManager.
    /// </summary>
    /// <param name="uiElement">The UI element to be registered.</param>
    /// <param name="playerNumber">The player number (determined by the PlayerNumber enumerator).</param>
    private void RegisterJoinLabel(GameObject uiElement, int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                _p1JoinLabel = uiElement;
                break;
            case 2:
                _p2JoinLabel = uiElement;
                break;
            case 3:
                _p3JoinLabel = uiElement;
                break;
            case 4:
                _p4JoinLabel = uiElement;
                break;
        }
    }

    /// <summary>
    /// Registers a color picker with this MainMenuManager.
    /// </summary>
    /// <param name="uiElement">The UI element to be registered.</param>
    /// <param name="playerNumber">The player number (determined by the PlayerNumber enumerator).</param>
    private void RegisterColorPicker(GameObject uiElement, int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                _p1ColorPicker = uiElement;
                break;
            case 2:
                _p2ColorPicker = uiElement;
                break;
            case 3:
                _p3ColorPicker = uiElement;
                break;
            case 4:
                _p4ColorPicker = uiElement;
                break;
        }
    }

    /// <summary>
    /// Registers a ready label with this MainMenuManager.
    /// </summary>
    /// <param name="uiElement">The UI element to be registered.</param>
    /// <param name="playerNumber">The player number (determined by the PlayerNumber enumerator).</param>
    private void RegisterReadyLabel(GameObject uiElement, int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                _p1ReadyLabel = uiElement;
                break;
            case 2:
                _p2ReadyLabel = uiElement;
                break;
            case 3:
                _p3ReadyLabel = uiElement;
                break;
            case 4:
                _p4ReadyLabel = uiElement;
                break;
        }
    }

    /// <summary>
    /// Uses reflection to access a variable using a string value to reference the property name.
    /// </summary>
    /// <param name="propertyName">The name of the property the be accessed.</param>
    /// <returns>A GameObject if the property was found; null if the property was not found.</returns>
    private GameObject ReflectGameObject(string propertyName)
    {
        var thisObjType = this.GetType();
        return thisObjType.GetProperty(propertyName).GetValue(this) as GameObject;
    }

    /// <summary>
    /// Shows the designated player's join label in the main menu UI. Only functions if the new game menu
    /// is currently active in the hierarchy.
    /// </summary>
    /// <param name="player">An enumerator that designates which player's UI element to show.</param>
    private void ShowJoinLabel(PlayerNumber player)
    {
        // If the new game menu is currently displayed, run this function
        if (_newGameMenu.activeInHierarchy)
        {
            // Get the appropriate game objects for this operation
            GameObject joinLabel = ReflectGameObject($"{player.ToString()}JoinLabel");
            GameObject colorPicker = ReflectGameObject($"{player.ToString()}ColorPicker");
            GameObject readyLabel = ReflectGameObject($"{player.ToString()}ReadyLabel");

            // Show the desired UI element and hide the undesired ones
            joinLabel.SetActive(true);
            colorPicker.SetActive(false);
            readyLabel.SetActive(false);
        }
    }

    /// <summary>
    /// Shows the designated player's color picker in the main menu UI. Only functions if the new game menu
    /// is currently active in the hierarchy.
    /// </summary>
    /// <param name="player">An enumerator that designates which player's UI element to show.</param>
    private void ShowColorPicker(PlayerNumber player)
    {
        // If the new game menu is currently displayed, run this function
        if (_newGameMenu.activeInHierarchy)
        {
            // Get the appropriate game objects for this operation
            GameObject joinLabel = ReflectGameObject($"{player.ToString()}JoinLabel");
            GameObject colorPicker = ReflectGameObject($"{player.ToString()}ColorPicker");
            GameObject readyLabel = ReflectGameObject($"{player.ToString()}ReadyLabel");

            // Show the desired UI element and hide the undesired ones
            joinLabel.SetActive(false);
            colorPicker.SetActive(true);
            readyLabel.SetActive(false);
        }
    }

    /// <summary>
    /// Shows the designated player's ready label in the main menu UI. Only functions if the new game menu
    /// is currently active in the hierarchy.
    /// </summary>
    /// <param name="player">An enumerator that designates which player's UI element to show.</param>
    private void ShowReadyLabel(PlayerNumber player)
    {
        // If the new game menu is currently displayed, run this function
        if (_newGameMenu.activeInHierarchy)
        {
            // Get the appropriate game objects for this operation
            GameObject joinLabel = ReflectGameObject($"{player.ToString()}JoinLabel");
            GameObject colorPicker = ReflectGameObject($"{player.ToString()}ColorPicker");
            GameObject readyLabel = ReflectGameObject($"{player.ToString()}ReadyLabel");

            // Show the desired UI element and hide the undesired ones
            joinLabel.SetActive(false);
            colorPicker.SetActive(false);
            readyLabel.SetActive(true);
        }
    }
}
