using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BbHopUp : MonoBehaviour
{
    public float BackspinDrag;
    private float ActualVelocityInXZ;
    private Rigidbody rg;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ActualVelocityInXZ = rg.velocity.magnitude - rg.velocity.y;//velocidade so no XZ
        rg.AddForce(BackspinDrag * Mathf.Sqrt(ActualVelocityInXZ) * Time.deltaTime * transform.up);// o transform.up é a rotação pra cima local
    }
}
