using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitSplash : MonoBehaviour
{
    public GameObject splashEffect;
    private bool hasCollided = false;
    private bool hasCollidedSecond = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion splashRotation = Quaternion.LookRotation(contact.normal);

            Destroy(Instantiate(splashEffect, contact.point, splashRotation), 3f);
            hasCollided = true;
        }
        else if (!hasCollidedSecond)
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion splashRotation = Quaternion.LookRotation(contact.normal);
            Destroy(Instantiate(splashEffect, contact.point, splashRotation), 3f);
            hasCollidedSecond = true;
        }
    }
}
