using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;

[CustomEditor(typeof(LayerData))]
public class LayerDataEditor : Editor
{
    private LayerData _layerData;
    private void OnEnable()
    {
        _layerData = (LayerData)serializedObject.targetObject;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("UPDATE LIST"))
        {
            _layerData.UpdateSortingLayersList();
        }
        if (GUILayout.Button("Reset Background on variables"))
        {
            _layerData.SetBackgroundLayersOnVariables();
        }
    }
}
