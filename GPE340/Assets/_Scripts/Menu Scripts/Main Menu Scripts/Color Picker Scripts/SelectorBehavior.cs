using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectorBehavior : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject _selectorBox; // The selector box associated with this color swatch
                                     // (Automatically assigns itself at runtime from RegisterSelectorBox)1444
    private PlayerObjectGroupData _objGroupData;
    private Image _image;
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
        // Component reference assignments
        _objGroupData = this.transform.GetComponentInParent<PlayerObjectGroupData>();
        _image = this.gameObject.GetComponent<Image>();

        _image.color = SkinManager.skinMgr.GetRGBColor(_objGroupData.colorPickers.IndexOf(this.gameObject));
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        // TODO: Implement selection script
        int index = _objGroupData.colorPickers.IndexOf(this.gameObject) + 1;

        if (IsSelected)
        {
            _selected = false;
            SkinManager.skinMgr.skinColors[index].SetSelected(false);
            EnableSameRowSelectors();
            EnableSameColorSelectors(_objGroupData.colorPickers.IndexOf((this.gameObject)));
        }
        else
        {
            _selected = true;
            SkinManager.skinMgr.skinColors[index].SetSelected(true);
            DisableSameRowSelectors();
            DisableSameColorSelectors(_objGroupData.colorPickers.IndexOf(this.gameObject));
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
        _image.color = SkinManager.skinMgr.skinColors[_objGroupData.colorPickers.IndexOf(this.gameObject) + 1].color;
        _selectorBox.SetActive(false);
    }

    private void DisableSameColorSelectors(int selectorIndex)
    {
        int playerNumber = this.gameObject.GetComponentInParent<PlayerObjectGroupData>().PlayerNumber;
        SelectorBehavior otherSelector;
        if (playerNumber != 1)
        {
            otherSelector = MainMenuManager.mainMenuMgr.P1ObjectGroup.colorPickers[selectorIndex].GetComponent<SelectorBehavior>();
            otherSelector.DisableSelector();
        }

        if (playerNumber != 2)
        {
            otherSelector = MainMenuManager.mainMenuMgr.P2ObjectGroup.colorPickers[selectorIndex].GetComponent<SelectorBehavior>();
            otherSelector.DisableSelector();
        }

        if (playerNumber != 3)
        {
            otherSelector = MainMenuManager.mainMenuMgr.P3ObjectGroup.colorPickers[selectorIndex].GetComponent<SelectorBehavior>();
            otherSelector.DisableSelector();
        }

        if (playerNumber != 4)
        {
            otherSelector = MainMenuManager.mainMenuMgr.P4ObjectGroup.colorPickers[selectorIndex].GetComponent<SelectorBehavior>();
            otherSelector.DisableSelector();
        }
    }

    private void EnableSameColorSelectors(int selectorIndex)
    {
        int playerNumber = this.gameObject.GetComponentInParent<PlayerObjectGroupData>().PlayerNumber;
        SelectorBehavior otherSelector;
        if (playerNumber != 1)
        {
            otherSelector = MainMenuManager.mainMenuMgr.P1ColorPicker.GetComponentInParent<PlayerObjectGroupData>()
                .colorPickers[selectorIndex].GetComponent<SelectorBehavior>();
            otherSelector.EnableSelector();
        }

        if (playerNumber != 2)
        {
            otherSelector = MainMenuManager.mainMenuMgr.P2ColorPicker.GetComponentInParent<PlayerObjectGroupData>()
                .colorPickers[selectorIndex].GetComponent<SelectorBehavior>();
            otherSelector.EnableSelector();
        }

        if (playerNumber != 3)
        {
            otherSelector = MainMenuManager.mainMenuMgr.P3ColorPicker.GetComponentInParent<PlayerObjectGroupData>()
                .colorPickers[selectorIndex].GetComponent<SelectorBehavior>();
            otherSelector.EnableSelector();
        }

        if (playerNumber != 4)
        {
            otherSelector = MainMenuManager.mainMenuMgr.P4ColorPicker.GetComponentInParent<PlayerObjectGroupData>()
                .colorPickers[selectorIndex].GetComponent<SelectorBehavior>();
            otherSelector.EnableSelector();
        }
    }

    private void DisableSameRowSelectors()
    {
        foreach (GameObject picker in _objGroupData.colorPickers )
        {
            if (picker != this.gameObject)
            {
                picker.GetComponent<SelectorBehavior>().DisableSelector();
            }
        }
    }

    private void EnableSameRowSelectors()
    {
        List<PlayerObjectGroupData> otherRowObjData = GetOtherRowObjGroups(_objGroupData);

        foreach (GameObject picker in _objGroupData.colorPickers)
        {
            if (picker != this.gameObject)
            {
                int thisIndex = _objGroupData.colorPickers.IndexOf(picker);

                if (!otherRowObjData[0].colorPickers[thisIndex].GetComponent<SelectorBehavior>().IsSelected &&
                    !otherRowObjData[1].colorPickers[thisIndex].GetComponent<SelectorBehavior>().IsSelected &&
                    !otherRowObjData[2].colorPickers[thisIndex].GetComponent<SelectorBehavior>().IsSelected)
                {
                    picker.GetComponent<SelectorBehavior>().EnableSelector();
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
}
