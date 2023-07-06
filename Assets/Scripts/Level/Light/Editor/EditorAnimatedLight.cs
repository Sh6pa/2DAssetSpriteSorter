using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AnimatedLight))]
public class EditorAnimatedLight : Editor
{
    override public void OnInspectorGUI()
    {
        var targetedScript = serializedObject.targetObject as AnimatedLight;

        // Permatent variables
        targetedScript.m_spriteColor = EditorGUILayout.ColorField(targetedScript.m_spriteColor);
        targetedScript.m_isAnimated = GUILayout.Toggle(targetedScript.m_isAnimated, "Is Animated");

        // Depending on boolean 
        if (targetedScript.m_isAnimated)
        {
            targetedScript.m_maxDelayOfRangeRefresh = EditorGUILayout.Slider("Max Refresh rate", targetedScript.m_maxDelayOfRangeRefresh, 0.1f, 2f);
            targetedScript.m_outerRadiusRange = EditorGUILayout.Slider("Outer Radius Range", targetedScript.m_outerRadiusRange, 0f, 1.5f);
            targetedScript.m_innerRadiusRange = EditorGUILayout.Slider("Inner Radius Range", targetedScript.m_innerRadiusRange, 0f, 1f);
        }
    }
}