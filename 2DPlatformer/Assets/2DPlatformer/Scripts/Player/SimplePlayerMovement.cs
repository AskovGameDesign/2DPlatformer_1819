using UnityEngine;
using System.Collections;

public class SimplePlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private Vector2 groundCheckBox = new Vector2(0.6f, 0.2f);
    [SerializeField] private LayerMask groundLayers;

    private Rigidbody2D rb2d;
    private float moveInput;
    private bool isGrounded;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
		moveInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapBox(groundCheckPosition.position, groundCheckBox, 0f, groundLayers);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = Vector2.up * jumpForce;
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
