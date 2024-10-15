using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLaser : MonoBehaviour
{
    public LineRenderer lineRenderer;  // Used to draw the laser
    public GameObject hitEffect;       // Prefab of the red point to instantiate at the hit location    // The point from where the laser is shot
    public float laserDistance = 50f;  // Max distance of the laser

    private GameObject currentHitEffect;

    void Start()
    {
        // Initialize the LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    void LateUpdate()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        // Start the ray from the fire point going forward
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, laserDistance))
        {
            // Set the position of the laser to go from the fire point to the hit point
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            // If we hit something, create the red point at the hit location
            if (currentHitEffect == null)
            {
                currentHitEffect = Instantiate(hitEffect, hit.point, Quaternion.identity);
            }
            else
            {
                currentHitEffect.transform.position = hit.point; // Move the red point to the hit location
            }
        }
        else
        {
            // If nothing is hit, set the laser end point to max distance
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * laserDistance);

            // Destroy the hit effect if no object is hit
            if (currentHitEffect != null)
            {
                Destroy(currentHitEffect);
            }
        }
    }
}
