using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : EnemyBase 
{

    [Range(1f, 5f)]
    public float upVelocity = 1f;


    public LayerMask groundLayer;

    Rigidbody2D rb2d;

    [HideInInspector]
    public bool moveUp = false;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0f, upVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{
        if (1 << collision.gameObject.layer == groundLayer)
        {
            //Debug.Log("Stone on ground");
            Invoke("MoveUp", 1f);
        }
	}

    private void FixedUpdate()
    {
        if(moveUp)
        {
            rb2d.velocity = new Vector2(0f, upVelocity);
        }
    }

    public void MoveUp()
    {
        //rb2d.velocity = new Vector2(0f, upVelocity);
        moveUp = true;
    }

    public void FlipMovingDirection()
    {
        //if (rb2d.velocity.y > 0f)
        //    rb2d.velocity = new Vector2(0f, downVelocity);
        //else
        //rb2d.velocity = new Vector2(0f, upVelocity);

        moveUp = false;
    }
}
