using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NewGameUIElementType
{
    None = 0,
    ObjectGroup = 1,
    JoinLabel = 2,
    ColorPicker = 3,
    ReadyLabel = 4
}

public class RegisterNewGameMenuElement : MonoBehaviour
{
    [Tooltip("The type of UI element this game object is."),
        SerializeField] private NewGameUIElementType _uiElementType;

    // Start is called before the first frame update
    private void Start()
    { // Component reference assignment
        PlayerObjectGroupData _pickerData = this.gameObject.GetComponentInParent<PlayerObjectGroupData>();

        switch (_uiElementType)
        {
            case NewGameUIElementType.None:
#if UNITY_EDITOR
                // Yells at me if I forget to set an object's element type, but only in the editor
                Debug.Log($"UI Element {this.gameObject.name} did not have its UI element type set.");
#endif
                break;
            default:
                // Registers this game object in the appropriate variable in the MainMenuManager
                MainMenuManager.mainMenuMgr.RegisterUIElement(this.gameObject, _pickerData.PlayerNumber, _uiElementType);
                break;
        }
    }
}
