using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MenuManager
{
    public static MainMenuManager mainMenuMgr;

    #region Private Properties
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

    // These individual values are stored so that they can be enabled/disabled easily
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
    #endregion

    #region Public Properties
    /// <summary>
    /// The Player 1 object group (for mass enabling/disabling of said objects)
    /// </summary>
    public PlayerObjectGroupData P1ObjectGroup
    {
        get { return _p1ObjectGroup; }
    }
    /// <summary>
    /// The Player 2 object group (for mass enabling/disabling of said objects)
    /// </summary>
    public PlayerObjectGroupData P2ObjectGroup
    {
        get { return _p2ObjectGroup; }
    }
    /// <summary>
    /// The Player 3 object group (for mass enabling/disabling of said objects)
    /// </summary>
    public PlayerObjectGroupData P3ObjectGroup
    {
        get { return _p3ObjectGroup; }
    }
    /// <summary>
    /// The Player 4 object group (for mass enabling/disabling of said objects)
    /// </summary>
    public PlayerObjectGroupData P4ObjectGroup
    {
        get { return _p4ObjectGroup; }
    }
    #endregion

    public void Awake()
    {
        // Singleton patten for this game object
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
        
        _newGameMenu.SetActive(false);
        _optionsMenu.SetActive(false);
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
            GameObject joinLabel = ReflectGameObject($"_{player.ToString().ToLower()}JoinLabel");
            GameObject colorPicker = ReflectGameObject($"_{player.ToString().ToLower()}ColorPicker");
            GameObject readyLabel = ReflectGameObject($"_{player.ToString().ToLower()}ReadyLabel");

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
            GameObject joinLabel = ReflectGameObject($"_{player.ToString().ToLower()}JoinLabel");
            GameObject colorPicker = ReflectGameObject($"_{player.ToString().ToLower()}ColorPicker");
            GameObject readyLabel = ReflectGameObject($"_{player.ToString().ToLower()}ReadyLabel");

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
            GameObject joinLabel = ReflectGameObject($"_{player.ToString().ToLower()}JoinLabel");
            GameObject colorPicker = ReflectGameObject($"_{player.ToString().ToLower()}ColorPicker");
            GameObject readyLabel = ReflectGameObject($"_{player.ToString().ToLower()}ReadyLabel");

            // Show the desired UI element and hide the undesired ones
            joinLabel.SetActive(false);
            colorPicker.SetActive(false);
            readyLabel.SetActive(true);
        }
    }
}
