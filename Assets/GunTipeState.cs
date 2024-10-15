using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTypeState : MonoBehaviour
{
    [Range(-1, 3)] 
    public int indexGun = 0; // 0 = rifle, 1 = shotgun, 2 = glock, 3 = 1911, -1 = hands
    public GameObject[] guns; 

    void Start()
    {
        SwitchGun(); 
    }

    void Update()
    {
        //keyGun(); // debug
        SwitchGun();
    }

    void keyGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            indexGun = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            indexGun = 1; 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            indexGun = 2; 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            indexGun = 3; 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            indexGun = -1;
        }
    }

    void SwitchGun()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(i == indexGun);
        }
        if (indexGun == -1)
        {
            foreach (GameObject gun in guns)
            {
                gun.SetActive(false);
            }
            guns[4].SetActive(true);
        }
    }
}
