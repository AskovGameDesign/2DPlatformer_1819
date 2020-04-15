using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor
{
    SerializedProperty platformMovementDirection;
    SerializedProperty platformMovementSpeed;
    SerializedProperty platformTransform;

    private void OnEnable()
    {
        platformMovementDirection = serializedObject.FindProperty("movementDirection");
        platformMovementSpeed = serializedObject.FindProperty("movementSpeed");
        platformTransform = serializedObject.FindProperty("platform");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(platformMovementDirection, new GUIContent("Platform direction"));

        EditorGUILayout.Slider(platformMovementSpeed, -5f, 5f, new GUIContent("Platform Speed"));

        EditorGUILayout.ObjectField(platformTransform, typeof(Transform), new GUIContent("Platform"));

        serializedObject.ApplyModifiedProperties();
    }


}
