using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialoggueManager : MonoBehaviour
{
    
    public Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    
}
