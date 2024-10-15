using System.Collections;
using UnityEngine;

public class delayedTrail : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    private float delayTime = 0.02f; //se a gameplay estiver com menos de 40 fps vai resultar em algo diferente

    void Start()
    {
        
        StartCoroutine(ActivateTrailWithDelay());
    }

    IEnumerator ActivateTrailWithDelay()
    {
        trailRenderer.enabled = false;
        yield return new WaitForSeconds(delayTime);
        trailRenderer.enabled = true;
        yield return null;
        enabled = false;
    }
}
