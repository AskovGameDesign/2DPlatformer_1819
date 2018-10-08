using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public Transform target;
    public bool drawTeleportLink = false;
    public Color teleportLinkColor = new Color(0.3f, 0.1f, 0.5f, 0.5f);

	void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.DownArrow))
        {
            collision.transform.position = target.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "gizmoTeleport.png", true);

        if(drawTeleportLink && target != null)
        {
            Gizmos.color = teleportLinkColor;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }

}
