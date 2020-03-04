using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorSelectorBehavior : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
    private PlayerObjectGroupData _objGroupData;
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

    public void Start()
    {
        _image.color = SkinManager.skinMgr.GetRGBColor(_objGroupData.ColorSelectors.IndexOf(this));
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        int index = _objGroupData.ColorSelectors.IndexOf(this) + 1;

        if (IsSelected)
        {
            _selected = false;
            // SkinManager.skinMgr.SkinColors[index].SetSelected(false);
            EnableSameRowSelectors();
            EnableSameColorSelectors(_objGroupData.ColorSelectors.IndexOf((this)));
            _objGroupData.ClearSelectedIndex();
        }
        else
        {
            _selected = true;
            // SkinManager.skinMgr.SkinColors[index].SetSelected(true);
            DisableSameRowSelectors();
            DisableSameColorSelectors(_objGroupData.ColorSelectors.IndexOf(this));
            _objGroupData.SetSelectedIndex(index);
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
        _image.color = SkinManager.skinMgr.SkinColors[0].Color;
        _selectorBox.SetActive(false);
    }

    /// <summary>
    /// Enables this selector and changes the selector's color to the appropriate color.
    /// </summary>
    private void EnableSelector()
    {
        _selectable = true;
        _image.color = SkinManager.skinMgr.SkinColors[_objGroupData.ColorSelectors.IndexOf(this) + 1].Color;
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
            MainMenuManager.mainMenuMgr.P1ObjectGroup.ColorSelectors[selectorIndex].DisableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P2)
        {
            MainMenuManager.mainMenuMgr.P2ObjectGroup.ColorSelectors[selectorIndex].DisableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P3)
        {
            MainMenuManager.mainMenuMgr.P3ObjectGroup.ColorSelectors[selectorIndex].DisableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P4)
        {
            MainMenuManager.mainMenuMgr.P4ObjectGroup.ColorSelectors[selectorIndex].DisableSelector();
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
            MainMenuManager.mainMenuMgr.P1ObjectGroup.SelectedIndex == -1)
        {
            MainMenuManager.mainMenuMgr.P1ObjectGroup.ColorSelectors[selectorIndex].EnableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P2 &&
            MainMenuManager.mainMenuMgr.P2ObjectGroup.SelectedIndex == -1)
        {
            MainMenuManager.mainMenuMgr.P2ObjectGroup.ColorSelectors[selectorIndex].EnableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P3 &&
            MainMenuManager.mainMenuMgr.P3ObjectGroup.SelectedIndex == -1)
        {
            MainMenuManager.mainMenuMgr.P3ObjectGroup.ColorSelectors[selectorIndex].EnableSelector();
        }
        if (_objGroupData.PlayerNumber != PlayerNumbers.P4 &&
            MainMenuManager.mainMenuMgr.P4ObjectGroup.SelectedIndex == -1)
        {
            MainMenuManager.mainMenuMgr.P4ObjectGroup.ColorSelectors[selectorIndex].EnableSelector();
        }
    }

    /// <summary>
    /// Disables all other color selectors for this player.
    /// </summary>
    private void DisableSameRowSelectors()
    {
        foreach (ColorSelectorBehavior picker in _objGroupData.ColorSelectors)
        {
            if (picker != this && picker.IsSelected == false)
            {
                picker.DisableSelector();
            }
        }
    }

    private void EnableSameRowSelectors()
    {
        List<PlayerObjectGroupData> otherRowObjData = GetOtherRowObjGroups(_objGroupData);

        foreach (ColorSelectorBehavior picker in _objGroupData.ColorSelectors)
        {
            if (picker != this)
            {
                int thisIndex = _objGroupData.ColorSelectors.IndexOf(picker);

                if (!otherRowObjData[0].ColorSelectors[thisIndex].IsSelected &&
                    !otherRowObjData[1].ColorSelectors[thisIndex].IsSelected &&
                    !otherRowObjData[2].ColorSelectors[thisIndex].IsSelected)
                {
                    picker.EnableSelector();
                }
            }
        }
    }

    private List<PlayerObjectGroupData> GetOtherRowObjGroups(PlayerObjectGroupData thisRowObjGroup)
    {
        List<PlayerObjectGroupData> otherRowObjGroupData = new List<PlayerObjectGroupData>();

        if (thisRowObjGroup == MainMenuManager.mainMenuMgr.P1ObjectGroup)
        {
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P2ObjectGroup);
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P3ObjectGroup);
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P4ObjectGroup);
        }
        else if (thisRowObjGroup == MainMenuManager.mainMenuMgr.P2ObjectGroup)
        {
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P1ObjectGroup);
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P3ObjectGroup);
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P4ObjectGroup);
        }
        else if (thisRowObjGroup == MainMenuManager.mainMenuMgr.P3ObjectGroup)
        {
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P1ObjectGroup);
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P2ObjectGroup);
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P4ObjectGroup);
        }
        else if (thisRowObjGroup == MainMenuManager.mainMenuMgr.P4ObjectGroup)
        {
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P1ObjectGroup);
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P2ObjectGroup);
            otherRowObjGroupData.Add(MainMenuManager.mainMenuMgr.P3ObjectGroup);
        }

        return otherRowObjGroupData;
    }

    public void SetObjGroupData(PlayerObjectGroupData objGroupData)
    {
        _objGroupData = objGroupData;
    }
}
