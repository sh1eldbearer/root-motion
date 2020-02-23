using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorSelectorBehavior : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _selectorBox; // The selector box associated with this color swatch
                                     // (Automatically assigns itself at runtime from RegisterColorSelectorBox)1444
    [SerializeField] private PlayerObjectGroupData _objGroupData;
    [SerializeField] private Image _image;
    private bool _selectable = true;
    private bool _selected = false;

    public bool IsSelectable
    {
        get { return _selectable; }
    }
    public bool IsSelected
    {
        get { return _selected; }
    }

    public void Start()
    {
        _image.color = SkinManager.skinMgr.GetRGBColor(_objGroupData.colorSelectors.IndexOf(this));
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        int index = _objGroupData.colorSelectors.IndexOf(this) + 1;

        if (IsSelected)
        {
            _selected = false;
            SkinManager.skinMgr.skinColors[index].SetSelected(false);
            EnableSameRowSelectors();
            EnableSameColorSelectors(_objGroupData.colorSelectors.IndexOf((this)));
        }
        else
        {
            _selected = true;
            SkinManager.skinMgr.skinColors[index].SetSelected(true);
            DisableSameRowSelectors();
            DisableSameColorSelectors(_objGroupData.colorSelectors.IndexOf(this));
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

    public void RegisterSelectorBox(GameObject selector)
    {
        _selectorBox = selector;
    }

    private void DisableSelector()
    {
        _selectable = false;
        _image.color = SkinManager.skinMgr.skinColors[0].color;
        _selectorBox.SetActive(false);
    }

    private void EnableSelector()
    {
        _selectable = true;
        _image.color = SkinManager.skinMgr.skinColors[_objGroupData.colorSelectors.IndexOf(this) + 1].color;
        _selectorBox.SetActive(false);
    }

    private void DisableSameColorSelectors(int selectorIndex)
    {
        int playerNumber = _objGroupData.PlayerNumber;
        ColorSelectorBehavior otherColorSelector;
        if (playerNumber != 1)
        {
            otherColorSelector = MainMenuManager.mainMenuMgr.P1ObjectGroup.colorSelectors[selectorIndex];
            otherColorSelector.DisableSelector();
        }

        if (playerNumber != 2)
        {
            otherColorSelector = MainMenuManager.mainMenuMgr.P2ObjectGroup.colorSelectors[selectorIndex];
            otherColorSelector.DisableSelector();
        }

        if (playerNumber != 3)
        {
            otherColorSelector = MainMenuManager.mainMenuMgr.P3ObjectGroup.colorSelectors[selectorIndex];
            otherColorSelector.DisableSelector();
        }

        if (playerNumber != 4)
        {
            otherColorSelector = MainMenuManager.mainMenuMgr.P4ObjectGroup.colorSelectors[selectorIndex];
            otherColorSelector.DisableSelector();
        }
    }

    private void EnableSameColorSelectors(int selectorIndex)
    {
        int playerNumber = _objGroupData.PlayerNumber;
        ColorSelectorBehavior otherColorSelector;
        if (playerNumber != 1)
        {
            otherColorSelector = MainMenuManager.mainMenuMgr.P1ObjectGroup.colorSelectors[selectorIndex];
            otherColorSelector.EnableSelector();
        }

        if (playerNumber != 2)
        {
            otherColorSelector = MainMenuManager.mainMenuMgr.P2ObjectGroup.colorSelectors[selectorIndex];
            otherColorSelector.EnableSelector();
        }

        if (playerNumber != 3)
        {
            otherColorSelector = MainMenuManager.mainMenuMgr.P3ObjectGroup.colorSelectors[selectorIndex];
            otherColorSelector.EnableSelector();
        }

        if (playerNumber != 4)
        {
            otherColorSelector = MainMenuManager.mainMenuMgr.P4ObjectGroup.colorSelectors[selectorIndex];
            otherColorSelector.EnableSelector();
        }
    }

    private void DisableSameRowSelectors()
    {
        foreach (ColorSelectorBehavior picker in _objGroupData.colorSelectors )
        {
            if (picker != this)
            {
                picker.DisableSelector();
            }
        }
    }

    private void EnableSameRowSelectors()
    {
        List<PlayerObjectGroupData> otherRowObjData = GetOtherRowObjGroups(_objGroupData);

        foreach (ColorSelectorBehavior picker in _objGroupData.colorSelectors)
        {
            if (picker != this.gameObject)
            {
                int thisIndex = _objGroupData.colorSelectors.IndexOf(picker);

                if (!otherRowObjData[0].colorSelectors[thisIndex].IsSelected &&
                    !otherRowObjData[1].colorSelectors[thisIndex].IsSelected &&
                    !otherRowObjData[2].colorSelectors[thisIndex].IsSelected)
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
