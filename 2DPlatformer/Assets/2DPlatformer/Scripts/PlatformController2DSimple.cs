using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformController2DSimple : MonoBehaviour 
{
    public float movementForce = 3f;
    public float jumpForce = 4f;
    //public float jumpTime = 2f;

    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;
    public Transform groundCheck;

    public GameObject playerDiedPrefab;
    public int numberOfPlayerLives = 3;
    public AudioClip jumpUpSound;
    public AudioClip jumpDownSound;

    Rigidbody2D rb2d;
    AudioSource audioSource;
    SpriteRenderer[] allSpriteRenders;
    CircleCollider2D collider2d;

    RaycastHit2D grounded;
    bool isGrounded;
    bool jump = false;
    float horizontalMovement;
    //bool jumpButtonPressed = false;
    //bool jumping = false;
    bool facingRight = false;
    Vector2 restartPosition;

    GameManager gameManger;

    float raycastRadius = 0.1f;

    private void OnEnable()
    {
		// Subscribe to events
		EnemyBase.OnHitByEnemy += EnemyBase_OnHitByEnemy;
    }

    private void OnDisable()
    {
		// Unsubscribe to events
		EnemyBase.OnHitByEnemy -= EnemyBase_OnHitByEnemy;
    }
    // Use this for initialization
    void Awake () 
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        allSpriteRenders = GetComponentsInChildren<SpriteRenderer>();
        collider2d = GetComponent<CircleCollider2D>();
        // Set the restart position to match the players start position 
        RestartPosition = transform.position;

        gameManger = FindObjectOfType<GameManager>();
        gameManger.UpdatePlayerLives(numberOfPlayerLives);
	}
	
	// Update is called once per frame
	void Update () 
    {
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, raycastRadius, 1 << LayerMask.NameToLayer("Ground"));
        horizontalMovement = Input.GetAxis("Horizontal");

        // Flip //
        if (horizontalMovement < 0f && facingRight == false)
            FlipPlayer();
        else if (horizontalMovement > 0f && facingRight == true)
            FlipPlayer();


        if (!isGrounded)
            return;

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlatformEffector2D pf2D = grounded.transform.GetComponent<PlatformEffector2D>();

            if(pf2D)
            {
                //pf2D.surfaceArc *= -1f;
                audioSource.PlayOneShot(jumpDownSound);
                StartCoroutine(StepDownFromPlatform(pf2D));
            }
        }


    }



    void FixedUpdate()
    {
        // Early out if this body type is NOT Dynamic
        if (rb2d.bodyType != RigidbodyType2D.Dynamic)
            return;
        
        rb2d.velocity = new Vector2(horizontalMovement * movementForce, rb2d.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded) //(Input.GetButtonDown("Jump") && grounded == true)
        {
            rb2d.velocity = Vector2.up * jumpForce;

            audioSource.PlayOneShot(jumpUpSound);
        }

        if (rb2d.velocity.y < 0f)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (rb2d.velocity.y > 0f && !Input.GetButton("Jump"))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }

        if (grounded && grounded.collider.tag == "Platform")
        {

            rb2d.velocity += grounded.rigidbody.velocity;
        }
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1f;

        transform.localScale = localScale;
    }

    IEnumerator StepDownFromPlatform(PlatformEffector2D _pf2D)
    {
        float storredSurfaceArc = _pf2D.surfaceArc;
        
        _pf2D.surfaceArc = storredSurfaceArc * -1;

        int exitCount = 0;

        while(isGrounded || exitCount > 1000)
        {
            exitCount++;
            yield return new WaitForEndOfFrame();
        }

        Debug.Log(isGrounded + " -- " + exitCount);
        _pf2D.surfaceArc = storredSurfaceArc;
    }



    void EnemyBase_OnHitByEnemy(Vector3 hitPosition)
    {
        if(playerDiedPrefab)
            Instantiate(playerDiedPrefab, hitPosition, Quaternion.identity);

        numberOfPlayerLives -= 1;

        gameManger.UpdatePlayerLives(numberOfPlayerLives);

        if(!isPlayerDead())
        {
            StartCoroutine(PlayerLostLife());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public Vector2 RestartPosition
    {
        set { restartPosition = value; }

        get { return restartPosition; }
    }

    public RaycastHit2D Grounded { get => grounded; set => grounded = value; }

    IEnumerator PlayerLostLife()
    {
        for (int i = 0; i < allSpriteRenders.Length; i++)
        {
            allSpriteRenders[i].enabled = false;
        }
        collider2d.enabled = false;

        rb2d.bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(3);

        transform.position = RestartPosition;

		for (int i = 0; i < allSpriteRenders.Length; i++)
		{
			allSpriteRenders[i].enabled = true;
		}
		collider2d.enabled = true;

        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    public bool isPlayerDead()
    {
        if (numberOfPlayerLives > 0)
            return false;
        else
            return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Gizmos.color = Color.green : Gizmos.color = Color.red;

       Gizmos.DrawWireSphere(groundCheck.position, raycastRadius);

        if (!rb2d)
            return;
                
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, new Vector3(rb2d.velocity.x, rb2d.velocity.y, 0f));

        
    }
}
