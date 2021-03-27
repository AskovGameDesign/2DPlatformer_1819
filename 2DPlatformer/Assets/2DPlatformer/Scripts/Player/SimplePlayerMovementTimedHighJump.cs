using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovementTimedHighJump : MonoBehaviour
{
    [Header("Movement and Jumping")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpTime; // time the player are allowed to jump up

    [Header("Groundcheck")]
    [SerializeField] private Vector2 groundCheckBox = new Vector2(0.6f, 0.2f);
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Color groundCheckColor = Color.magenta;

    private Rigidbody2D rb2d;
    private float movement;
    private bool isGrounded;
    private bool isJumping;
    private float jumpTimeCounter;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPosition.position, groundCheckBox, 0f, groundLayers);

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
        
        rb2d.velocity = new Vector2(movement * movementSpeed, rb2d.velocity.y);
    }

    // Only used to draw our ground check area //
    private void OnDrawGizmos()
    {
        if (!this.isActiveAndEnabled)
            return;

        Gizmos.color = groundCheckColor;
        Gizmos.DrawCube(groundCheckPosition.position, new Vector3(groundCheckBox.x, groundCheckBox.y, 0f));
    }
}
