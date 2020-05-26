/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Sentence))]
public class SentenceEditor : Editor
{

    SerializedProperty sentenceSprite;

    private void OnEnable()
    {
       

        sentenceSprite = serializedObject.FindProperty("sprite");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(sentenceSprite, new GUIContent("Hello World"));

        //base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();

    }
}
*/