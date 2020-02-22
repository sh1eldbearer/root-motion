using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RegisterColorSelectorBox : MonoBehaviour
{
    void Awake()
    {
        // Registers the selector box with its parent script and disables itself afterward
        this.GetComponentInParent<ColorSelectorBehavior>().RegisterSelectorBox(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
