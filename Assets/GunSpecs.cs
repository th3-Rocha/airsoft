using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GunSpecs : MonoBehaviour
{
    [Header("Tipo Carregador (ammo-Shotgun, ammo-rifle, ammo-pistola-1911, ammo-pistola_glock)")]
    public String tagAmmoName;
    [Header("Modelo (nome específico do modelo)")]
    public String gunRealName;

    [Header("HOP-UP ajustável")]
    public float hopUp;
    [Header("Força da mola ajustável em Joules")]
    public float energyInJoules;

    [Header("Motor (rotação em RPM)")]
    public float fireRate;
    [Header("Max Imprecision Walking")]

    public float maxImprecision; //rifle 0.1 shotgun é 0.25 e pistol é 0.06
    public float minImprecision; //rifle 0.0 shotgun é 0.05 e pistol é 0
    [Header("Extras")]
    public bool itsHaveAutoFire;
    public int bbsPerShoot = 1;
    public GameObject BBsPrefabCarregador;
    [Header("Sounds")]
    public AudioClip shootSound;
    public AudioClip reloadingSounds;
    public Color bbBulletColor = Color.green;
     [Header("Cartucho Config")]
    public int maxAmmo;
    public float bbsAmmoMass;

    public int cartuchoExtraPickup;

}
