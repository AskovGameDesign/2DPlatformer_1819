using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : EnemyBase 
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            HitByEnemy(collision.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            HitByEnemy(collision.contacts[0].point);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "gizmoDeath.png", true);

    }
}
