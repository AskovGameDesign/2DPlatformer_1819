using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public bool flipAnimation = true;

    int points = 1;

    float flipTimer = 0f;
    float flipValue = 0f;
    Vector2 localScale;
    float destroyDelay = 0f;

    AudioSource audioSource;
    SpriteRenderer sr;
    Collider2D c2D;

    GameManager gm;

    // Use this for initialization
	void Awake () 
    {
        localScale = transform.localScale;
        gm = FindObjectOfType<GameManager>();

        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        c2D = GetComponent<Collider2D>();

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (flipAnimation)
        {
            flipValue = Mathf.Sin(flipTimer);
            transform.localScale = new Vector2(flipValue * localScale.x, localScale.y);
            flipTimer += Time.deltaTime;
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (gm)
                gm.AddPoints(points);

            if(audioSource != null && audioSource.clip != null)
            {
                destroyDelay = audioSource.clip.length;

                audioSource.Play();

                if (sr) sr.enabled = false;

                if (c2D) c2D.enabled = false;
            }

            Destroy(this.gameObject, destroyDelay);
        }
    }
}
