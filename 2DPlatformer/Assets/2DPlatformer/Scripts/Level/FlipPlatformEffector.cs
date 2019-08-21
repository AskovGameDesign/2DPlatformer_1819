using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlatformEffector2D))]
public class FlipPlatformEffector : MonoBehaviour
{

    PlatformEffector2D platformEffector2D;

    // Use this for initialization
    void Start()
    {
        platformEffector2D = GetComponent<PlatformEffector2D>();
    }

    public void FlipEffector()
    {
        platformEffector2D.surfaceArc *= -1f;
    }

    public void FlipFlopEffector()
    {
        StartCoroutine(FlipFlop());
    }

    IEnumerator FlipFlop()
    {
        float surfaceArc = platformEffector2D.surfaceArc;
        platformEffector2D.surfaceArc = surfaceArc * -1f;
        yield return new WaitForSeconds(0.1f);
    }
}
