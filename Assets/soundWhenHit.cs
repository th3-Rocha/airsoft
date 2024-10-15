using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundWhenHit : MonoBehaviour
{
    public GameObject splashEffect;

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion splashRotation = Quaternion.LookRotation(contact.normal);

        Destroy(Instantiate(splashEffect, contact.point, splashRotation), 3f);

    }
}
