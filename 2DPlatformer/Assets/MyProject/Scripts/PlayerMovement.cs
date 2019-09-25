using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    public Transform groundCheckPosition;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundLayers;

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
		
		isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, groundLayers);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = Vector2.up * jumpForce;
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
    }
}
