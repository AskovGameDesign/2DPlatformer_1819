using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
	public Conversation conversation;

    // Start is called before the first frame update
    void Start()
    {
        if(conversation != null)
		{
            Debug.Log(conversation.sentences[0].text);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
