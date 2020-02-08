using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MenuManager
{
    [Header("Panel Objects")]
    [Tooltip("The GameObject containing the panel for the main menu's new game menu."),
        SerializeField] private GameObject newGameMenu;
    [Tooltip("The GameObject containing the panel for the main menu's options menu."), 
        SerializeField] private GameObject optionsMenu;

    [Header("Player Group Container Objects")]
    [Tooltip(""),
     SerializeField] private GameObject p1Objects;
    [Tooltip(""),
     SerializeField]
    private GameObject p2Objects;
    [Tooltip(""),
     SerializeField]
    private GameObject p3Objects;
    [Tooltip(""),
     SerializeField]
    private GameObject p4Objects;

    [Header("Player 1 Objects")]
    [Tooltip(""),
     SerializeField]
    private GameObject p1JoinLabel;
    [Tooltip(""),
     SerializeField]
    private GameObject p1ColorSelector;
    [Tooltip(""),
     SerializeField]
    private GameObject p1ReadyLabel;
    
    [Header("Player 2 Objects")]
    [Tooltip(""),
     SerializeField]
    private GameObject p2JoinLabel;
    [Tooltip(""),
     SerializeField]
    private GameObject p2ColorSelector;
    [Tooltip(""),
     SerializeField]
    private GameObject p2ReadyLabel;

    [Header("Player 3 Objects")]
    [Tooltip(""),
     SerializeField]
    private GameObject p3JoinLabel;
    [Tooltip(""),
     SerializeField]
    private GameObject p3ColorSelector;
    [Tooltip(""),
     SerializeField]
    private GameObject p3ReadyLabel;

    [Header("Player 4 Objects")]
    [Tooltip(""),
     SerializeField]
    private GameObject p4JoinLabel;
    [Tooltip(""),
     SerializeField]
    private GameObject p4ColorSelector;
    [Tooltip(""),
     SerializeField]
    private GameObject p4ReadyLabel;
}
