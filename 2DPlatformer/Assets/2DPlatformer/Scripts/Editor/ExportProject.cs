using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class ExportProject : EditorWindow 
{
    string projectFileName;

	// Add menu named "Export Project" to the Window/Stuff menu
	[MenuItem("Window/Stuff/Export Project")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		ExportProject window = (ExportProject)EditorWindow.GetWindow(typeof(ExportProject));
		window.Show();
	}

	void OnGUI()
	{
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);
		projectFileName = EditorGUILayout.TextField("Package Name", projectFileName);

        if(GUILayout.Button("Export Package"))
        {
            if( String.IsNullOrEmpty(projectFileName) )
            {
                projectFileName = "TheWholeProject";
            }
            ExportAllProject(projectFileName);
        }
	}

    void ExportAllProject(string _saveName)
    {
		string[] projectContent = AssetDatabase.GetAllAssetPaths();
        AssetDatabase.ExportPackage(projectContent, _saveName + ".unitypackage", ExportPackageOptions.Recurse | ExportPackageOptions.IncludeLibraryAssets);
        //AssetDatabase.ExportPackage(projectContent, _saveName + ".unitypackage", ExportPackageOptions.IncludeLibraryAssets );
		Debug.Log("Project Exported");
    }
}
