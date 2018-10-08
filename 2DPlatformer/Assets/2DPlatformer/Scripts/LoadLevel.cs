using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
	
    public void Load(int _levelId)
    {
        SceneManager.LoadScene(_levelId);
    }

    public void Load(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }

}
