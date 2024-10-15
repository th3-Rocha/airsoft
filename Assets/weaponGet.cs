using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponGet : MonoBehaviour
{
    // Public variables to hold the three tags and boolean states
    private string[] weaponTags = { "ak74m", "shotgun", "pistol", "pistol1911" };
    private string[] ammoTags = { "ammo-rifle", "ammo-shotgun", "ammo-pistol_glock", "ammo-pistola-1911" };
    public GunTypeState GTS;
    public BBsGunFire BGF;
    private InputStateMachine ISM;
    public int gunIndex = -1;
    public Transform dropGunPivot;
    public GameObject[] gunsPrefab;
    public float timeCdGet = 2.0f;
    private float currentCdGet = 0f;
    void Start(){
        BGF = GetComponent<BBsGunFire>();
        ISM =  GetComponent<InputStateMachine>();
    }
    void Update()
    {
        if (currentCdGet > 0)
        {
            currentCdGet -= Time.deltaTime;
        }

        GTS.indexGun = gunIndex;
        if (Input.GetKeyDown(KeyCode.G) && gunIndex != -1 && !ISM.isReloading)
        {
            currentCdGet = timeCdGet;
            GameObject gun = Instantiate(gunsPrefab[gunIndex], dropGunPivot.position, dropGunPivot.rotation);
            gun.GetComponent<Rigidbody>().velocity = dropGunPivot.right + dropGunPivot.forward * 3;
            Debug.Log("Dropped " + weaponTags[gunIndex] + "!");
            gunIndex = -1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gunIndex == -1 && currentCdGet <= 0)
        {
            if (other.CompareTag(weaponTags[0]))
            {
                gunIndex = 0;
                Debug.Log("Picked up ak74m!");
                Destroy(other.gameObject);
            }
            else if (other.CompareTag(weaponTags[1]))
            {
                gunIndex = 1;
                Debug.Log("Picked up shotgun!");
                Destroy(other.gameObject);
            }
            else if (other.CompareTag(weaponTags[2]))
            {
                gunIndex = 2;
                Debug.Log("Picked up pistol!");
                Destroy(other.gameObject);
            }
            else if (other.CompareTag(weaponTags[3]))
            {
                gunIndex = 3;
                Debug.Log("Picked up pistol Colt 1911!");
                Destroy(other.gameObject);
            }
            currentCdGet = timeCdGet;
        }
        if (other.CompareTag(ammoTags[0]))
        {
            BGF.extraCartuchos[0] += BGF.cartuchoExtraPickup[0];
            Debug.Log("Picked up ak74m ammo");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag(ammoTags[1]))
        {
            BGF.extraCartuchos[1] += BGF.cartuchoExtraPickup[1];
            Debug.Log("Picked up shotgun ammo");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag(ammoTags[2]))
        {
            BGF.extraCartuchos[2] += BGF.cartuchoExtraPickup[2];
            Debug.Log("Picked up pistol ammo");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag(ammoTags[3]))
        {
            Debug.Log("Picked up pistol Colt 1911 ammo");
            BGF.extraCartuchos[3] += BGF.cartuchoExtraPickup[3];
            Destroy(other.gameObject);
        }
    }
}
