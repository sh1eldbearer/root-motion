using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MenuManager
{
    [Header("Menu Objects")]
    [Tooltip("The GameObject containing the panel for the main menu's start menu."),
        SerializeField] private GameObject startGameMenu;
    [Tooltip("The GameObject containing the panel for the main menu's options menu."), 
        SerializeField] private GameObject optionsMenu;
}
