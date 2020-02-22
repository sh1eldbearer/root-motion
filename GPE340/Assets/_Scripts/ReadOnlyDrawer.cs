using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * Displays a property in the Unity inspector without making it editable by designers.
 */

// Defines custom attribute tag
public class ReadOnlyAttribute : PropertyAttribute
{

}

// Defines custom property drawer
#if UNITY_EDITOR || UNITY_EDITOR_64
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    // Gets the size of the element for rendering in the inspector
    public override float GetPropertyHeight(SerializedProperty property,
        GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    // Generates the property drawer
    public override void OnGUI(Rect position, SerializedProperty property,
        GUIContent label)
    {
        string displayStr;

        // Formats the property for display
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                displayStr = property.intValue.ToString();
                break;
            case SerializedPropertyType.Boolean:
                displayStr = property.intValue.ToString();
                break;
            case SerializedPropertyType.Float:
                displayStr = property.floatValue.ToString("0.00000");
                break;
            case SerializedPropertyType.String:
                displayStr = property.stringValue;
                break;
            case SerializedPropertyType.Enum:
                displayStr = property.enumDisplayNames[property.enumValueIndex];
                break;
            case SerializedPropertyType.ObjectReference:
                displayStr = property.objectReferenceValue.ToString();
                break;
            default:
                displayStr = "(not supported)";
                break;
        }

        // Displays the property
        GUI.enabled = false;
        EditorGUI.LabelField(position, label.text, displayStr, GUI.skin.label);
        GUI.enabled = true;
    }
}
#endif
