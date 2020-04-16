using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformController : MonoBehaviour 
{
    public float movementForce = 3f;
    public float jumpForce = 4f;
    
    public float fallMultiplier = 3f;
    public float lowJumpMultiplier = 2f;
    public Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckBox = new Vector2(0.6f, 0.2f);

    public GameObject playerDiedPrefab;
    public int numberOfPlayerLives = 3;
    public AudioClip jumpUpSound;
    public AudioClip jumpDownSound;


    #region Private variables
    Rigidbody2D rb2d;
    AudioSource audioSource;
    BoxCollider2D playerCollider;

    bool isGrounded;
    Collider2D groundCollider;
    SpriteRenderer[] allSpriteRenders;
    float horizontalMovement;
    bool facingRight = false;
    #endregion

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
    void Start () 
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        allSpriteRenders = GetComponentsInChildren<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();
       
        GameManager.Instance.UpdatePlayerLives(numberOfPlayerLives);
	}
	
	// Update is called once per frame
	void Update () 
    {
        groundCollider = Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0f, groundLayer);
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

        _pf2D.surfaceArc = storredSurfaceArc;
    }


    void EnemyBase_OnHitByEnemy(Vector3 hitPosition)
    {
        if(playerDiedPrefab)
            Instantiate(playerDiedPrefab, hitPosition, Quaternion.identity);

        numberOfPlayerLives -= 1;

        GameManager.Instance.UpdatePlayerLives(numberOfPlayerLives);

        if(!IsPlayerDead())
        {
            StartCoroutine(PlayerLostLife());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator PlayerLostLife()
    {
        for (int i = 0; i < allSpriteRenders.Length; i++)
        {
            allSpriteRenders[i].enabled = false;
        }
        playerCollider.enabled = false;

        rb2d.bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(3);

        transform.position = GameManager.Instance.RestartPosition;

		for (int i = 0; i < allSpriteRenders.Length; i++)
		{
			allSpriteRenders[i].enabled = true;
		}
		playerCollider.enabled = true;

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
        if (!this.isActiveAndEnabled || groundCheck == null)
            return;

        //Gizmos.color = isGrounded ? Gizmos.color = Color.green : Gizmos.color = Color.red;
        Color gc = isGrounded ? gc = Color.green : gc = Color.red;
        gc.a = 0.5f;
        Gizmos.color = gc;
        Gizmos.DrawCube(groundCheck.position, groundCheckBox);

    }
}
