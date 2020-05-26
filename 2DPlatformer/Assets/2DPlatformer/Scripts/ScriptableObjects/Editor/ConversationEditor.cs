/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Conversation))]
public class ConversationEditor : Editor
{
    Conversation conversation;

    private void OnEnable()
    {
        if (conversation == null)
            conversation = (Conversation)target;
    }

    public override void OnInspectorGUI()
    {

        for (int i = 0; i < conversation.sentences.Length; i++)
        {
            conversation.sentences[i].sprite = (Sprite)EditorGUILayout.ObjectField(new GUIContent("Sprite"), conversation.sentences[i].sprite, typeof(Sprite), false);

            conversation.sentences[i].text = EditorGUILayout.TextArea(conversation.sentences[i].text);
            //conversation.sentences[i].text = EditorGUILayout.TextArea(new GUIContent("Sentence"), conversation.sentences[i].text);

            conversation.sentences[i].enableConversationBranch = EditorGUILayout.Toggle(new GUIContent("Branch"), conversation.sentences[i].enableConversationBranch);

            EditorGUILayout.Space();
        }


        base.OnInspectorGUI();
        
    }
}
*/