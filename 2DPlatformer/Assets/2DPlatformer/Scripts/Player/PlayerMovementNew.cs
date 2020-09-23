using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementNew : MonoBehaviour
{
    [Header("Player Control")]
    [Range(1f, 10f)]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 8f;
    [SerializeField] private float fallMultiplier = 4f;
    [SerializeField] private float lowJumpMultiplier = 2.5f;

    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheckCenter;
    [SerializeField] private Vector2 groundCheckBoxSize = new Vector2(1f, 0.1f);
    [SerializeField] private LayerMask groundLayer;

    [Header("Physics Settings")]
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float linearDrag = 6f;

    Vector2 movementDirection;
    Rigidbody2D rb;
    bool jumpRequest;
    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapBox(groundCheckCenter.position, groundCheckBoxSize, 0f, groundLayer);

        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
        }
    }

    void Movement()
    {
        rb.AddForce(Vector2.right * movementDirection.x * speed);

        // clamp vores hastighed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void ModifyPhysics()
    {
        // check om vi bevæger os til højre og trykker mod venstre og modsat
        bool directionChange = (rb.velocity.x < 0f && movementDirection.x > 0f) || (rb.velocity.x > 0f && movementDirection.x < 0f);
        // sæt en høj drag værdi (=hurtigere stop) hvis vi slipper vores retningstast :-) eller skifter retning
        // hvis ingen af delene, så sætter vi bare vores drag til 0 
        if (Mathf.Abs(movementDirection.x) < 0.4f || directionChange)
            rb.drag = linearDrag;
        else
            rb.drag = 0f;
    }

    private void FixedUpdate()
    {
        // Apply movement
        Movement();
        // Apply the physics modifications
        ModifyPhysics();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(groundCheckCenter.position, new Vector3(groundCheckBoxSize.x, groundCheckBoxSize.y, 0f));
    }
}
