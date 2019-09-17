using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl_HighJump : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    public Transform groundCheckPosition;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayers;

    private Rigidbody2D rb2d;
    private float movement;
    private bool isGrounded;
    private bool isJumping;
    public float jumpTime;
    private float jumpTimeCounter;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayers);

        movement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }

        if(Input.GetKey(KeyCode.Space) && isJumping)
        {
            if(jumpTimeCounter > 0f)
            {
                rb2d.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
            isJumping = false;
    }

    void FixedUpdate()
    {
        
        rb2d.velocity = new Vector2(movement * speed, rb2d.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
    }
}
