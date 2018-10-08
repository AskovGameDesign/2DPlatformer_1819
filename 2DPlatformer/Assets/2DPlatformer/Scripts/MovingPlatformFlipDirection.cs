using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MovingPlatformFlipDirection : MonoBehaviour 
{

    BoxCollider2D collider2d;

    [HideInInspector]
    public MovingPlatform movingPlatformScript;

    void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
        if (collider2d)
            collider2d.isTrigger = true;

        movingPlatformScript = gameObject.GetComponentInParent<MovingPlatform>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if(movingPlatformScript != null)
        {
            if(collision.tag == "Platform")
            {
                movingPlatformScript.movementSpeed *= -1f;
            }
        }
    }

    void OnDrawGizmos()
    {
        if(collider2d == null)
            collider2d = GetComponent<BoxCollider2D>();

        Gizmos.color = new Color(0.8f, 0.8f, 0f, 0.2f);
        Gizmos.DrawCube(transform.position + new Vector3(collider2d.offset.x, collider2d.offset.y, 0f), collider2d.size);
    }
}
