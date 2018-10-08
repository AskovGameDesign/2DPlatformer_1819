using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour
{

    public delegate void HitByEnemyHandler(Vector3 hitPosition);
    public static event HitByEnemyHandler OnHitByEnemy;



    public void HitByEnemy(Vector3 hitposition)
    {
        if(OnHitByEnemy != null)
        {
            OnHitByEnemy(hitposition);
        }
    }


}
