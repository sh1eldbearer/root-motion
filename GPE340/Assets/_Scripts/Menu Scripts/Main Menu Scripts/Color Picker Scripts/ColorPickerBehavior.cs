using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility.Enums;

public class ColorPickerBehavior : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The selector box object associated with this color swatch"),
     SerializeField]
    private GameObject _selectorBox;
    [Tooltip("The Image component for this selector box."),
     SerializeField]
    private Image _image;
    [Tooltip("The data component of the object group this selector is associated with."),
     SerializeField]
    private ObjectGroupData _objGroupData;
    [Tooltip("Whether or not this selector is able to be selected. (Essentially, whether this selector is \"enabled\" " +
             "or \"disabled\".)"),
     SerializeField]
#pragma warning restore CS0649
    private bool _selectable = true;
    [Tooltip("Whether or not this selector has been selected. (A player has chosen this color for their avatar.)"),
     SerializeField]
    private bool _selected = false;
    #endregion

    #region Public Properties
    /// <summary>
    /// Whether or not this selector is able to be selected. (Essentially, whether this selector is "enabled"
    /// or "disabled".)
    /// </summary>
    public bool IsSelectable
    {
        get { return _selectable; }
    }

    /// <summary>
    /// Whether or not this selector has been selected. (A player has chosen this color for their avatar.)
    /// </summary>
    public bool IsSelected
    {
        get { return _selected; }
    }
    #endregion

    // Awake is called before Start
    private void Awake()
    {
        // Component reference assignments
        if (_objGroupData)
        {
            _objGroupData = this.gameObject.GetComponentInParent<ObjectGroupData>();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _image.color = SkinManager.skinMgr.GetRGBColor(_objGroupData.ColorPickers.IndexOf(this));
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        int index = _objGroupData.ColorPickers.IndexOf(this);
        if (IsSelectable)
        {
            if (IsSelected)
            {
                _selected = false;

                EnableSameRowSelectors();
                EnableSameColorSelectors(_objGroupData.ColorPickers.IndexOf((this)));

                // Clears the skin color selection from the object group and the player data
                GameManager.gm.PlayerInfo[(int) _objGroupData.PlayerNumber].ClearSkinColorIndex();
                GameManager.gm.PlayerInfo[(int) _objGroupData.PlayerNumber].SetStatus(PlayerStatus.Joined);
            }
            else
            {
                // Marks this 
                _selected = true;

                DisableSameRowSelectors();
                DisableSameColorSelectors(_objGroupData.ColorPickers.IndexOf(this));

                // Tells the player data which skin color was selected
                GameManager.gm.PlayerInfo[(int) _objGroupData.PlayerNumber].SetSkinColorIndex(index);
            }

            UpdateGameReadyStatus();
        }
    }

    public void OnPointerEnter(PointerEventData pointerData)
    {
        if (IsSelectable)
        {
            _selectorBox.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData pointerData)
    {
        if (IsSelectable && !IsSelected)
        {
            _selectorBox.SetActive(false);
        }
    }

    /// <summary>
    /// Disables this selector, and changes the selector's color to the disabled color.
    /// </summary>
    private void DisableSelector()
    {
        _selectable = false;
        _image.color = SkinManager.skinMgr.GetRGBColor(8);
        _selectorBox.SetActive(false);
    }

    /// <summary>
    /// Enables this selector and changes the selector's color to the appropriate color.
    /// </summary>
    private void EnableSelector()
    {
        _selectable = true;
        _image.color = SkinManager.skinMgr.GetRGBColor(_objGroupData.ColorPickers.IndexOf(this));
        _selectorBox.SetActive(false);
    }

    /// <summary>
    /// Disables all other color selectors representing the same color as this color selector.
    /// </summary>
    /// <param name="selectorIndex">The index of the color selector (from PlayerObjectGroupData.ColorSelectors)
    /// that was selected.</param>
    private void DisableSameColorSelectors(int selectorIndex)
    {
        if (_objGroupData.PlayerNumber != PlayerNumbers.P1)
        {
            MainMenuManager.mainMenuMgr.P1ObjectGroup.ColorPickers[selectorIndex].DisableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P2)
        {
            MainMenuManager.mainMenuMgr.P2ObjectGroup.ColorPickers[selectorIndex].DisableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P3)
        {
            MainMenuManager.mainMenuMgr.P3ObjectGroup.ColorPickers[selectorIndex].DisableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P4)
        {
            MainMenuManager.mainMenuMgr.P4ObjectGroup.ColorPickers[selectorIndex].DisableSelector();
        }
    }

    /// <summary>
    /// Enables all other color selectors representing the same color as this color selector,
    /// provided there is not another color currently selected by that player.
    /// </summary>
    /// <param name="selectorIndex">The index of the color selector (from PlayerObjectGroupData.ColorSelectors)
    /// that was unselected.</param>
    private void EnableSameColorSelectors(int selectorIndex)
    {
        if (_objGroupData.PlayerNumber != PlayerNumbers.P1 && 
            MainMenuManager.mainMenuMgr.P1ObjectGroup.gameObject.activeInHierarchy &&
            GameManager.gm.PlayerInfo[0].SkinColorIndex == -1)
        {
            MainMenuManager.mainMenuMgr.P1ObjectGroup.ColorPickers[selectorIndex].EnableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P2 &&
            MainMenuManager.mainMenuMgr.P2ObjectGroup.gameObject.activeInHierarchy &&
            GameManager.gm.PlayerInfo[1].SkinColorIndex == -1)
        {
            MainMenuManager.mainMenuMgr.P2ObjectGroup.ColorPickers[selectorIndex].EnableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P3 &&
            MainMenuManager.mainMenuMgr.P3ObjectGroup.gameObject.activeInHierarchy &&
            GameManager.gm.PlayerInfo[2].SkinColorIndex == -1)
        {
            MainMenuManager.mainMenuMgr.P3ObjectGroup.ColorPickers[selectorIndex].EnableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P4 &&
            MainMenuManager.mainMenuMgr.P4ObjectGroup.gameObject.activeInHierarchy &&
            GameManager.gm.PlayerInfo[3].SkinColorIndex == -1)
        {
            MainMenuManager.mainMenuMgr.P4ObjectGroup.ColorPickers[selectorIndex].EnableSelector();
        }
    }

    /// <summary>
    /// Disables all other color selectors for this player.
    /// </summary>
    private void DisableSameRowSelectors()
    {
        foreach (ColorPickerBehavior picker in _objGroupData.ColorPickers)
        {
            // As long as the current picker isn't this one, disable it
            if (picker != this && picker.IsSelected == false)
            {
                picker.DisableSelector();
            }
        }
    }
    
    /// <summary>
    /// Enables all other eligible color selectors for this player.
    /// </summary>
    private void EnableSameRowSelectors()
    {
        List<ObjectGroupData> otherRowObjData = GetOtherRowObjGroups(_objGroupData);

        foreach (ColorPickerBehavior picker in _objGroupData.ColorPickers)
        {
            // As long as the current picker isn't this one...
            if (picker != this)
            {
                int pickerIndex = _objGroupData.ColorPickers.IndexOf(picker);

                // Check to see if the current picker's color was selected in any other row (or if those rows aren't currently enabled)...
                if ((otherRowObjData[0].gameObject.activeInHierarchy == false || 
                     otherRowObjData[0].ColorPickers[pickerIndex].IsSelected == false) &&
                    (otherRowObjData[0].gameObject.activeInHierarchy == false || 
                     otherRowObjData[1].ColorPickers[pickerIndex].IsSelected == false) &&
                    (otherRowObjData[0].gameObject.activeInHierarchy == false || 
                     otherRowObjData[2].ColorPickers[pickerIndex].IsSelected == false))
                {
                    // If not, enable it
                    picker.EnableSelector();
                }
            }
        }
    }

    /// <summary>
    /// Gets a list of the other players' ObjectGroupData components.
    /// </summary>
    /// <param name="thisRowObjGroup">This player's ObjectGroupData component.</param>
    /// <returns>A list of the ObjectGroupData components for all other players.</returns>
    private List<ObjectGroupData> GetOtherRowObjGroups(ObjectGroupData thisRowObjGroup)
    {
        List<ObjectGroupData> otherRowObjGroupData = new List<ObjectGroupData>();

        if (thisRowObjGroup != MainMenuManager.mainMenuMgr.P1ObjectGroup)
        {
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P1ObjectGroup);
        }
        if (thisRowObjGroup != MainMenuManager.mainMenuMgr.P2ObjectGroup)
        {
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P2ObjectGroup);
        }
        if (thisRowObjGroup != MainMenuManager.mainMenuMgr.P3ObjectGroup)
        {
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P3ObjectGroup);
        }
        if (thisRowObjGroup != MainMenuManager.mainMenuMgr.P4ObjectGroup)
        {
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P4ObjectGroup);
        }

        return otherRowObjGroupData;
    }

    /// <summary>
    /// Tells this color picker which ObjectGroup it's a member of.
    /// </summary>
    /// <param name="objGroupData">The ObjectGroup this color selector is to be listed on.</param>
    public void SetObjGroupData(ObjectGroupData objGroupData)
    {
        _objGroupData = objGroupData;
    }

    /// <summary>
    /// Waits until all joined players have selected a color before activating the Start Game button.
    /// </summary>
    private void UpdateGameReadyStatus()
    {
        // All players that have joined the game must have selected a color in order for the game to start
        if (CheckPlayerReadyStatus(GameManager.gm.PlayerInfo[0]) &&
            CheckPlayerReadyStatus(GameManager.gm.PlayerInfo[1]) &&
            CheckPlayerReadyStatus(GameManager.gm.PlayerInfo[2]) &&
            CheckPlayerReadyStatus(GameManager.gm.PlayerInfo[3]))
        {
            MainMenuManager.mainMenuMgr.StartGameButton.interactable = true;
        }
        else
        {
            MainMenuManager.mainMenuMgr.StartGameButton.interactable = false;
        }
    }
    
    /// <summary>
    /// Checks to see if a player has joined the game, and is ready to begin the game.
    /// </summary>
    /// <param name="playerTracking">The data for the player that is being checked.</param>
    /// <returns>True if the player has not joined the game, or is ready.</returns>
    private bool CheckPlayerReadyStatus(PlayerTracking playerTracking)
    {
        if ((int)playerTracking.Status < 0 || playerTracking.Status == PlayerStatus.Ready)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Checks to see if a player has joined the game, and is ready to begin the game.
    /// </summary>
    /// <param name="playerStatus">The status for the player that is being checked.</param>
    /// <returns>True if the player has not joined the game, or is ready.</returns>
    private bool CheckPlayerReadyStatus(PlayerStatus playerStatus)
    {
        if ((int)playerStatus < 0 || playerStatus == PlayerStatus.Ready)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
