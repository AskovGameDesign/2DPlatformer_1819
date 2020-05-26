using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[System.Serializable]
public class Sentence
{
    public Sprite sprite;
    [TextArea(2, 5)]
    public string text;

    public string contineText = "Continue";
    public bool enableConversationBranch;


    public Sentence()
    {
        enableConversationBranch = false;
    }

    

    public Conversation branchConversation;
}

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
        EditorGUILayout.TextField("Sentence Editor");
        EditorGUILayout.PropertyField(sentenceSprite, new GUIContent("Hello World"));

        //base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();

    }
}




[CreateAssetMenu(fileName = "New conversation", menuName = "Create Conversation")]
public class Conversation : ScriptableObject
{
    public Sentence[] sentences;
}



