using UnityEngine;
using UnityEditor;

// Horizontal line implementation courtesy of Madgvox on the Unity forums: 
// https://forum.unity.com/threads/horizontal-line-in-editor-window.520812/
public static class MiscEditorMethods
{
    static readonly GUIStyle horizontalLine = new();
    public static void HorizontalLine ( Color color ) 
    {
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
        horizontalLine.margin = new RectOffset(0, 0, 4, 4);
        horizontalLine.fixedHeight = 1;

        var oldColor = GUI.color;
        GUI.color = color;
        GUILayout.Box( GUIContent.none, horizontalLine );
        GUI.color = oldColor;
    }

    public static void Multispace (int times)
    {
        for (int i = 0; i < times; i++) EditorGUILayout.Space();
    }
}