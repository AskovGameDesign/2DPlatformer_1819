using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FallingStoneFlipDirection : MonoBehaviour {

    BoxCollider2D bc2d;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FallingStone>())
            collision.gameObject.GetComponent<FallingStone>().moveUp = false; //.FlipMovingDirection();
    }

	void OnDrawGizmos()
	{
        if (bc2d == null)
            bc2d = GetComponent<BoxCollider2D>();

		Gizmos.color = new Color(0.8f, 0.1f, 0.8f, 0.2f);
        Gizmos.DrawCube(transform.position + new Vector3(bc2d.offset.x, bc2d.offset.y, 0f), bc2d.size);
	}
}
