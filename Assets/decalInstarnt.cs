using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decalInstarnt : MonoBehaviour
{
    // The decal GameObject to instantiate
    public GameObject decalMagAk;
    public GameObject decalShotgunShell;
    public Transform decalOutputShotgun;
    public GameObject decalPistolMag;
    private GunTypeState GTS;

    // Start is called before the first frame update
    void Start()
    {
        GTS = GetComponent<GunTypeState>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to instantiate the decal and destroy it after 5 seconds
    public void decalInst()
    {

        if (GTS.indexGun == 0)
        {
            GameObject decalInstance = Instantiate(decalMagAk, transform.position, transform.rotation);
            Destroy(decalInstance, 5f);
        }
        else if (GTS.indexGun == 1)
        {
            GameObject decalInstance = Instantiate(decalShotgunShell, decalOutputShotgun.position, decalOutputShotgun.rotation);
            decalInstance.GetComponent<Rigidbody>().velocity = decalInstance.GetComponent<Rigidbody>().velocity = -decalOutputShotgun.transform.right * -2; //cria uma força pra esquerda
            Destroy(decalInstance, 5f);
        }
        else if (GTS.indexGun == 2)
        {
        }
        else if (GTS.indexGun == -1)
        {
        }

    }
}
