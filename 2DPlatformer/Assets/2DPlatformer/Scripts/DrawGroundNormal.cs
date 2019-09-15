using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGroundNormal : MonoBehaviour
{
    public bool alignToNormal;

    public LayerMask groundcheckLayerMask;
    RaycastHit2D hit2d;

    Vector3 worldspacePoint;
    Vector2 hitNormal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        worldspacePoint = transform.localPosition + (Vector3.up * -3f);

        hit2d = Physics2D.Linecast(transform.position, worldspacePoint, groundcheckLayerMask);

        hitNormal = hit2d.normal.normalized;

        

        //Debug.DrawRay(transform.position, hit2d.normal.normalized * 3f, Color.yellow);
        Debug.DrawLine(transform.position, worldspacePoint, Color.magenta);

        Debug.DrawRay(transform.localPosition, hitNormal * 3f, Color.yellow);

        if(alignToNormal)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, hitNormal);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
        }
    }
}
