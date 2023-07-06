using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangeIntAttribute))]
public class RangeIntInspector : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // First get the attribute since it contains the range for the slider
        RangeIntAttribute range = attribute as RangeIntAttribute;
        // Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
        
        EditorGUI.IntSlider(position, property, (int)range.min, (int)range.max, label);
    }
}