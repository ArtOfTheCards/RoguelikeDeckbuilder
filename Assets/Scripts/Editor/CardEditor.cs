using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

// Sprite preview implementation courtesy of Peter on Sunny Valley Studio:
// https://www.sunnyvalleystudio.com/blog/unity-2d-sprite-preview-inspector-custom-editor
// Subclass selection menu implementation inspired by Valentin Simonov's article on Reorderable Lists:
// https://va.lent.in/unity-make-your-lists-functional-with-reorderablelist/

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    Card card;

    // ================================================================
    // Primary GUI methods
    // ================================================================

    private void OnEnable()
    {
        card = (Card)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Base Properties", EditorStyles.whiteLargeLabel);
        MiscEditorMethods.HorizontalLine(Color.gray);
        EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("debug_ID"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("title"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("art"));
            PreviewArt();
        EditorGUI.indentLevel--;

        MiscEditorMethods.Multispace(4);

        EditorGUILayout.LabelField("On Play", EditorStyles.whiteLargeLabel);
        MiscEditorMethods.HorizontalLine(Color.gray);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playTarget"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("playDescription"));
        EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("playEffects"));
            if (GUILayout.Button("Add New PlayEffect")) ShowAddMenu(card.playEffects);
        EditorGUI.indentLevel--;

        MiscEditorMethods.Multispace(4);

        EditorGUILayout.LabelField("On Throw", EditorStyles.whiteLargeLabel);
        MiscEditorMethods.HorizontalLine(Color.gray);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("throwTarget"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("throwDescription"));
        EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("throwEffects"));
            if (GUILayout.Button("Add New ThrowEffect")) ShowAddMenu(card.throwEffects);
        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }

    // ================================================================
    // Helper methods
    // ================================================================

    private void PreviewArt()
    {
        if (card.art == null) return;

        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), AssetPreview.GetAssetPreview(card.art));
    }

    private void ShowAddMenu(List<CardEffect> effectList)
    {
        GenericMenu menu = new();
        
        string[] guids = AssetDatabase.FindAssets("", new[]{"Assets/Scripts/Cards/CardEffects"});
        foreach (string guid in guids) 
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            menu.AddItem(
                new GUIContent(Path.GetFileNameWithoutExtension(path)), 
                false, 
                (object context) => 
                    {
                        switch (context as string)
                        {
                            case "AutoDrawEffect": effectList.Add(new AutoDrawEffect()); break;
                            case "CardCountDirectDamageEffect": effectList.Add(new CardCountDirectDamageEffect()); break;
                            case "DirectDamageEffect": effectList.Add(new DirectDamageEffect()); break;
                            case "DelayEffect": effectList.Add(new DelayEffect()); break;
                            case "SpawnEffect": effectList.Add(new SpawnEffect()); break;
                            default: 
                            {
                                Debug.LogError("Card Error: The CardEffect you attempted to add "
                                             + "has not been added to the switch statement in"
                                             + "CardEditor.ShowAddMenu()."); 
                                break;
                            }
                        }
                    }, 
                Path.GetFileNameWithoutExtension(path)
            );
        }

        menu.ShowAsContext();
    }
}