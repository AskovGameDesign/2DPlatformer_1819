using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingPlatform : MonoBehaviour {

    public enum MovementDirection
    {
        vertical,
        horisontal
    }
    public MovementDirection movementDirection = MovementDirection.horisontal;

    public float movementSpeed = 1f;

    public MovingPlatformFlipDirection[] flipDirections;

    public Transform platform; 

    public Vector2 platformVelocity;
    public bool showPlatformMovementDirection = true;


    Rigidbody2D rb2d;


    void Start () 
    {
        rb2d = GetComponentInChildren<Rigidbody2D>();

        if(flipDirections != null)
        {
            for (int i = 0; i < flipDirections.Length; i++)
            {
                flipDirections[i].movingPlatformScript = this;
            }
        }

        if (movementDirection == MovementDirection.horisontal)
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            
        }
        else if(movementDirection == MovementDirection.vertical)
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
            
    }

    // Update is called once per frame
    void FixedUpdate () 
    {
        if (movementDirection == MovementDirection.horisontal)
            rb2d.velocity = new Vector2(movementSpeed, 0f);
        else
            rb2d.velocity = new Vector2(0f, movementSpeed);
        //rb2d.MovePosition(rb2d.position + platformVelocity * Time.deltaTime);
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        //Vector2 normalizedVelocity = platformVelocity.normalized;
        //Gizmos.DrawRay(transform.position, new Vector3(platformVelocity.x, platformVelocity.y, 0f));

        if(flipDirections != null && platform != null)
        {
            for (int i = 0; i < flipDirections.Length; i++)
            {
                Gizmos.DrawLine(flipDirections[i].transform.position, platform.position);
            }
        }
    }

}
