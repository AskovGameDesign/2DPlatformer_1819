using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour 
{

    public float rotationSpeed = 2f;
    public float startDelay = 0f;

    bool rotateAllowed = false;

    void Start()
    {
        Invoke("EnableRotation", startDelay);
    }

    void EnableRotation()
    {
        rotateAllowed = true;
    }

    // Update is called once per frame
    void Update () 
    {
        if(rotateAllowed)
            transform.Rotate(new Vector3(0f, 0f, rotationSpeed));
	}
}
