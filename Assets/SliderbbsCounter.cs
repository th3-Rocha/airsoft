using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SliderbbsCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sliderAmmo;
    private BBsGunFire bBsGunFire;
    private GunTypeState gunTypeState;
    public TMP_Text MaxAmmo;
    public TMP_Text CurrentAmmo;
    public TMP_Text AmmoMass;

    public TMP_Text AmmoExtra;
    void Start()
    {
        gunTypeState = GetComponent<GunTypeState>();
        bBsGunFire = GetComponent<BBsGunFire>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gunTypeState.indexGun >= 0)
        {
            AmmoMass.text = (bBsGunFire.bbsAmmoMass[gunTypeState.indexGun] * 1000).ToString() + "g";
            AmmoMass.color = bBsGunFire.bbBulletColor[gunTypeState.indexGun];
            MaxAmmo.text = bBsGunFire.maxAmmo[gunTypeState.indexGun].ToString();
            AmmoExtra.text = bBsGunFire.extraCartuchos[gunTypeState.indexGun].ToString();
            CurrentAmmo.text = bBsGunFire.currentAmmo[gunTypeState.indexGun].ToString();
            sliderAmmo.maxValue = bBsGunFire.maxAmmo[gunTypeState.indexGun];
            sliderAmmo.value = bBsGunFire.currentAmmo[gunTypeState.indexGun];
        }
        else
        {
            MaxAmmo.text = 0.ToString();
            CurrentAmmo.text = 0.ToString();
        }

    }
}
