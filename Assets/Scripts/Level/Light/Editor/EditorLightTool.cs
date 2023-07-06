using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightTool))]
public class EditorLightTool : Editor
{
    private LightTool _lightTool;

    private void OnEnable()
    {
        _lightTool = (LightTool)serializedObject.targetObject;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Variables
        DrawLevelSectionVar();
        DrawLightDataVar();
        DrawLightSettingsVar();
       
        DrawDebugSection();
     
        // Functions
        DrawLayerSortingFunc();
        DrawLightSettingsFunc();
        DrawCreateLightsFunc();

        serializedObject.ApplyModifiedProperties();
    }

    #region Variable Draw
    private void DrawLevelSectionVar()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Level Section Data", EditorStyles.boldLabel);
        /*EditorGUILayout.PropertyField(serializedObject.FindProperty("_layerParams"))*/;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_layerData"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_levelSection"));
        EditorGUILayout.EndVertical();
    }
    private void DrawLightDataVar()
    {
        EditorGUILayout.LabelField("Light Data", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        LayerData t = (LayerData)serializedObject.FindProperty("_layerData").objectReferenceValue;
        GameObject go = (GameObject)serializedObject.FindProperty("_levelSection").objectReferenceValue; 
        if (t != null && go != null)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.IntSlider(serializedObject.FindProperty("_subdivisions"), 1, t.SortingLayers.Length-1);
            serializedObject.FindProperty("_subdivisions").intValue = Mathf.Clamp(serializedObject.FindProperty("_subdivisions").intValue, 0, t.SortingLayers.Length);
            if (EditorGUI.EndChangeCheck())
            {
                _lightTool.DestroyDebugLines();
                _lightTool.CreateDebugLines();
            }
        }
        // EditorGUILayout.PropertyField(serializedObject.FindProperty("_subdivisions"));
        EditorGUILayout.EndVertical();
    }
    private void DrawLightSettingsVar()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Light Settings", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_lightColor"));
        if (EditorGUI.EndChangeCheck())
        {
            Color tempColor = serializedObject.FindProperty("_lightColor").colorValue;
            tempColor.a = 1.0f;
            serializedObject.FindProperty("_lightColor").colorValue = tempColor;
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_volumeOpacityMultiplier"));
        EditorGUILayout.EndVertical();
    }
    private void DrawDebugSection()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_DepthPreview"));
        if (EditorGUI.EndChangeCheck())
        {
            if (!_lightTool.m_DepthPreview)
            {
                _lightTool.CreateDebugLines();
            }
            else
            {
                _lightTool.DestroyDebugLines();
            }
        }
        if (_lightTool.m_DepthPreview)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.IntSlider(serializedObject.FindProperty("_depthPreviewLayer"), 0, serializedObject.FindProperty("_subdivisions").intValue);
            serializedObject.FindProperty("_depthPreviewLayer").intValue = Mathf.Clamp(serializedObject.FindProperty("_depthPreviewLayer").intValue, 0, serializedObject.FindProperty("_subdivisions").intValue);
            // EditorGUILayout.PropertyField(serializedObject.FindProperty("_depthPreviewLayer"));
            if (EditorGUI.EndChangeCheck())
            {
                _lightTool.ShowDebugLines();
            }
        }
        EditorGUILayout.EndVertical();
    }
    #endregion
    #region Function Button Draw
    private void DrawLayerSortingFunc()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Layer Sorting", EditorStyles.boldLabel);
        if (GUILayout.Button("Sort Layers"))
        {
            _lightTool.SortLayers();
        }
        if (GUILayout.Button("Normalize Layers"))
        {
            _lightTool.NormalizeLayers();
        }
        EditorGUILayout.EndVertical();
    }
    private void DrawLightSettingsFunc()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Light Settings", EditorStyles.boldLabel);

        if (GUILayout.Button("Fade Background Color"))
            _lightTool.FadeBackgroundColor();

        if (GUILayout.Button("Fade Background Intensity"))
            _lightTool.FadeBackgroundIntensity();

        if (GUILayout.Button("Fade Volume Opacity"))
            _lightTool.FadeVolumeOpacity();
        EditorGUILayout.EndVertical();
    }
    private void DrawCreateLightsFunc()
    {
        LayerData t = (LayerData)serializedObject.FindProperty("_layerData").objectReferenceValue;
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Create Lights", EditorStyles.boldLabel);
        if (GUILayout.Button("Create Spot Light") && t!=null)
            _lightTool.EditorCreateLight(t.SpotLight);
        if (GUILayout.Button("Create Animated Spot Light") && t != null)
            _lightTool.EditorCreateLight(t.AnimatedSpotLight);
        if (GUILayout.Button("Create Sprite Light") && t != null)
            _lightTool.EditorCreateLight(t.SpriteLight);
        if (GUILayout.Button("Create Freeform Light") && t != null)
            _lightTool.EditorCreateLight(t.FreeFormLight);
        EditorGUILayout.EndVertical();
    }
    #endregion
}
