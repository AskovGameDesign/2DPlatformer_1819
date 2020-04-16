using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovementDoubleJump : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int numberOfJumps = 2;

    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private Vector2 groundCheckBox = new Vector2(0.6f, 0.2f);
    [SerializeField] private LayerMask groundLayers;

    private Rigidbody2D rb2d;
    private float moveInput;
    private bool isGrounded;
    private int jumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapBox(groundCheckPosition.position, groundCheckBox, 0f, groundLayers);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < numberOfJumps)
        {
            rb2d.velocity = Vector2.up * jumpForce;
            jumpCount+=1;
        }

        // Reset jumpcount when we are standing on the ground
        if (isGrounded && rb2d.velocity.y <= 0f)
        {
            jumpCount = 0;
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
    }

    // Only used to draw our ground check area //
    private void OnDrawGizmos()
    {
        if (!this.isActiveAndEnabled)
            return;

        Color gizmoColor = Color.magenta;
        gizmoColor.a = 0.5f;
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(groundCheckPosition.position, new Vector3(groundCheckBox.x, groundCheckBox.y, 0f));
    }
}
