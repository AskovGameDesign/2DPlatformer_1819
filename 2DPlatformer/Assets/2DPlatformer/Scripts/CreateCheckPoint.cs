using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CreateCheckPoint : MonoBehaviour 
{

    public BoxCollider2D bc2d;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if it is the Player that entered the trigger
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.RestartPosition = this.transform.position;
        }
    }

    // Draw the Trigger/Boxcollider so that it is visible in the Editor view //
	void OnDrawGizmos()
	{
		if (bc2d == null)
			bc2d = GetComponent<BoxCollider2D>();

		Gizmos.color = new Color(0.1f, 0.9f, 0.8f, 0.2f);
		Gizmos.DrawCube(transform.position + new Vector3(bc2d.offset.x, bc2d.offset.y, 0f), bc2d.size);
	}
}
