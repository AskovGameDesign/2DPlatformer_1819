using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateCheckPoint))]
public class CheckPointEditor : Editor
{
    CreateCheckPoint checkPointScript;

    float checkPointHeight;
    float checkPointWidth;

    private void OnEnable()
    {
        if (checkPointScript == null)
            checkPointScript = (CreateCheckPoint)target;

        checkPointScript.bc2d = checkPointScript.GetComponent<BoxCollider2D>();

        // Hide the boxcollider2D in the inspector
        checkPointScript.bc2d.hideFlags = HideFlags.HideInInspector;
        checkPointScript.bc2d.isTrigger = true;
        //checkPointScript.GetComponent<BoxCollider2D>().hideFlags = HideFlags.HideInInspector;

        checkPointWidth = checkPointScript.bc2d.size.x;
        checkPointHeight = checkPointScript.bc2d.size.y;
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        checkPointWidth = EditorGUILayout.Slider("Width", checkPointWidth, 0.5f, 10f);
        checkPointHeight = EditorGUILayout.Slider("Height", checkPointHeight, 0.5f, 10f);

        checkPointScript.bc2d.size = new Vector2(checkPointWidth, checkPointHeight);
    }
}
