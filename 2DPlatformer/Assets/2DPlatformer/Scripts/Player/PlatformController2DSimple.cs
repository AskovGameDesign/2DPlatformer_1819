using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformController2DSimple : MonoBehaviour 
{
    public float movementForce = 3f;
    public float jumpForce = 4f;
    
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
    BoxCollider2D collider2d;


    bool isGrounded;
    Collider2D groundCollider;

    float horizontalMovement;
    
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
        collider2d = GetComponent<BoxCollider2D>();
        // Set the restart position to match the players start position 
        RestartPosition = transform.position;

        gameManger = FindObjectOfType<GameManager>();

        if(gameManger)
            gameManger.UpdatePlayerLives(numberOfPlayerLives);
	}
	
	// Update is called once per frame
	void Update () 
    {
        groundCollider = Physics2D.OverlapCircle(groundCheck.position, raycastRadius, 1 << LayerMask.NameToLayer("Ground"));
        isGrounded = (bool)groundCollider;
        
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // Flip the player based on walking direction //
        if (horizontalMovement < 0f && facingRight == false)
            FlipPlayer();
        else if (horizontalMovement > 0f && facingRight == true)
            FlipPlayer();

    

        if(Input.GetKeyDown(KeyCode.DownArrow) && isGrounded)
        {
            PlatformEffector2D platformEffector2D = groundCollider.transform.GetComponent<PlatformEffector2D>();

            if(platformEffector2D)
            {
                //pf2D.surfaceArc *= -1f;
                audioSource.PlayOneShot(jumpDownSound);
                StartCoroutine(StepDownFromPlatform(platformEffector2D));
            }
        }

        // Jump code moved away from the fixedupdate function //
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.velocity = Vector2.up * jumpForce;

            audioSource.PlayOneShot(jumpUpSound);
        }

        // The player is falling //
        if (rb2d.velocity.y < 0f)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }
        // Check for quick release jump //
        else if (rb2d.velocity.y > 0f && !Input.GetButton("Jump"))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        }
    }



    void FixedUpdate()
    {
        // Early out if this body type is NOT Dynamic
        if (rb2d.bodyType != RigidbodyType2D.Dynamic)
            return;
        
        rb2d.velocity = new Vector2(horizontalMovement * movementForce, rb2d.velocity.y); 

        if (isGrounded && groundCollider.CompareTag("Platform"))
        {

            rb2d.velocity += groundCollider.attachedRigidbody.velocity;
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

        if(!IsPlayerDead())
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

    //public RaycastHit2D Grounded { get => grounded; set => grounded = value; }

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

    public bool IsPlayerDead()
    {
        if (numberOfPlayerLives > 0)
            return false;
        else
            return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Gizmos.color = Color.green : Gizmos.color = Color.red;

        if(groundCheck)
            Gizmos.DrawWireSphere(groundCheck.position, raycastRadius);

        if (!rb2d)
            return;
                
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, new Vector3(rb2d.velocity.x, rb2d.velocity.y, 0f));

        
    }
}
