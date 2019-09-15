using UnityEngine;
using System.Collections;


public class FlipEnemy : MonoBehaviour
{
    public Vector2 flipDirection = new Vector2(0.2f, 1f);
    public float force = 10f;
    Rigidbody2D rb2d;

	// Use this for initialization
	void Start()
	{
        rb2d = GetComponent<Rigidbody2D>();


    }

	// Update is called once per frame
	void Update()
	{
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FlipTheEnemy(force, 1f, -180f));
        }
	}




    IEnumerator FlipTheEnemy(float _force, float _flipDuration, float _flipTargetAngle)
    {
        rb2d.AddForce( flipDirection.normalized * _force, ForceMode2D.Impulse);

        float t = 0f;
        float dt = 0f;

        float flipAngle = 0f;

        while(dt <= _flipDuration )
        {
            t = Mathf.InverseLerp(0f, _flipDuration, dt); // 0 - 1 value
            dt += Time.deltaTime;

            flipAngle = Mathf.Lerp(0f, _flipTargetAngle, t);

            //transform.eulerAngles = new Vector3(0f, 0f, flipAngle);

            rb2d.rotation = flipAngle;

            //Debug.Log(t + " - " + flipAngle);

            yield return null;
        }

        rb2d.rotation = _flipTargetAngle;
    }

    private void OnDrawGizmos()
    {
        Vector2 normalizedFlipDirection = flipDirection.normalized;
        Vector3 rayDirection = transform.TransformDirection(new Vector3(normalizedFlipDirection.x, normalizedFlipDirection.y, 0f)) * 5f; 
        Gizmos.DrawRay(transform.position, rayDirection );
    }
}
