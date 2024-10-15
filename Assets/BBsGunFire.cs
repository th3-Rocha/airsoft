using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BBsGunFire : MonoBehaviour
{
    // index reference //0 = rifle // 1 = shotgun // 2 = glock // 3 = 1911
    [Header("Outros Scripts e Componentes")]
    public InputStateMachine ISM;
    public GunTypeState GTS;
    public decalInstarnt DI;

    [Header("Transform Bullet Output: 0 = Rifle, 1 = Shotgun, 2 = Glock , 3 = 1911")]
    [Tooltip("Ordem dos Index prefabs, 0 = Rifle, 1 = Shotgun, 2 = Glock , 3 = 1911")]
    public GameObject[] gunsPrefabConfig;
    public Transform[] BBsOutputTransform;
    private AudioSource audioSource;

    private float[] maxImprecision = new float[10]; //rifle 0.1 shotgun é 0.25 e pistol é 0.06
    private float[] minImprecision = new float[10]; //rifle 0.0 shotgun é 0.05 e pistol é 0
    [HideInInspector]
    public float[] fireRate = new float[10]; //10f , 3.5f e 8
    private float[] fireCooldown = new float[10];
    [HideInInspector]
    public float[] hopUp = new float[10];
    [HideInInspector]
    public int[] extraCartuchos = new int[10];
    [HideInInspector]
    public int[] cartuchoExtraPickup = new int[10];
    [HideInInspector]
    public float[] bbsAmmoMass = new float[10];
    [HideInInspector]
    public bool[] itsHaveAutoFire = new bool[10];
    [HideInInspector]
    public float[] energyInJoules = new float[10]; //default is 1.49f
    private int[] bbsPerShoot = new int[10];
    private AudioClip[] shootSound = new AudioClip[10];
    private AudioClip[] reloadingSounds = new AudioClip[10];
    private GameObject[] BBsPrefabCarregador = new GameObject[10];
    [HideInInspector]
    public Color[] bbBulletColor = new Color[10];
    [HideInInspector]
    public float velocity = 0;
    private bool hasPlayedReloadSound = false;

    [Header("Ammo Settings")]
    [HideInInspector]
    public int[] maxAmmo = new int[10];  // Maximum ammo for each gun
    [HideInInspector]
    public int[] currentAmmo = new int[10];  // Current ammo for each gun
    [HideInInspector]
    private bool isReloading = false;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // Initialize gun settings
        for (int i = 0; i < gunsPrefabConfig.Length; i++)
        {
            var gunSpecs = gunsPrefabConfig[i].GetComponent<GunSpecs>();
            if (gunSpecs != null)
            {
                maxImprecision[i] = gunSpecs.maxImprecision;
                minImprecision[i] = gunSpecs.minImprecision;
                fireRate[i] = gunSpecs.fireRate;
                energyInJoules[i] = gunSpecs.energyInJoules;
                bbsPerShoot[i] = gunSpecs.bbsPerShoot;
                shootSound[i] = gunSpecs.shootSound;
                reloadingSounds[i] = gunSpecs.reloadingSounds;
                itsHaveAutoFire[i] = gunSpecs.itsHaveAutoFire;
                BBsPrefabCarregador[i] = gunSpecs.BBsPrefabCarregador;
                hopUp[i] = gunSpecs.hopUp;
                bbsAmmoMass[i] = gunSpecs.bbsAmmoMass;
                // Initialize ammo
                bbBulletColor[i] = gunSpecs.bbBulletColor;
                maxAmmo[i] = gunSpecs.maxAmmo;
                currentAmmo[i] = maxAmmo[i];//coloca munição atual como a maxima
                extraCartuchos[i] = maxAmmo[i];//coloca munição extra como a maxima
                cartuchoExtraPickup[i] = gunSpecs.cartuchoExtraPickup;
            }
            else
            {
                Debug.LogWarning("GunSpecs component not found on " + gunsPrefabConfig[i].name);
            }
        }
    }

    void Update()
    {
        if (GTS.indexGun == -1)
        {
            // hands
        }
        else
        {
            if (extraCartuchos[GTS.indexGun] > 0)
            {
                ISM.canReload = true;
            }
            else
            {
                ISM.canReload = false;
            }
            fireCooldown[GTS.indexGun] -= Time.deltaTime;
            ISM.isShooting = false;
            if (!ISM.unableToShooting && !isReloading)
            {
                bool fireButtonPressed = itsHaveAutoFire[GTS.indexGun] ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");

                if (fireButtonPressed && fireCooldown[GTS.indexGun] <= 0f)
                {
                    if (currentAmmo[GTS.indexGun] > 0)
                    {

                        Shoot();
                        currentAmmo[GTS.indexGun]--;
                    }
                    else
                    {
                        //call outOfAmmoSound
                        Debug.Log("Out of ammo!");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                itsHaveAutoFire[GTS.indexGun] = !itsHaveAutoFire[GTS.indexGun];
            }
        }
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(shootSound[GTS.indexGun]);
        ISM.isShooting = true;

        for (int i = 0; i < bbsPerShoot[GTS.indexGun]; i++)
        {
            GameObject bb = Instantiate(BBsPrefabCarregador[GTS.indexGun], BBsOutputTransform[GTS.indexGun].position, BBsOutputTransform[GTS.indexGun].rotation);
            Rigidbody rb = bb.GetComponent<Rigidbody>();
            BbHopUp bbsHopUpComponent = bb.GetComponent<BbHopUp>();
            //calc formula de velocidade
            velocity = Mathf.Sqrt(2 * energyInJoules[GTS.indexGun] / rb.mass);
            //calc formula de velocidade

            //calc imprecision
            Vector3 randomDirection = BBsOutputTransform[GTS.indexGun].forward;
            float actualImprecision = Mathf.Clamp(ISM.imprecisionInput / maxImprecision[GTS.indexGun], minImprecision[GTS.indexGun], maxImprecision[GTS.indexGun]);
            randomDirection += new Vector3(UnityEngine.Random.Range(-actualImprecision, actualImprecision), UnityEngine.Random.Range(-actualImprecision, actualImprecision), UnityEngine.Random.Range(-actualImprecision, actualImprecision));
            //calc imprecision


            //change material albedo to red color
            Renderer bbRenderer = bb.GetComponent<Renderer>();
            bbRenderer.material.SetColor("_BaseColor", bbBulletColor[GTS.indexGun]);
            TrailRenderer bbTrail = bb.GetComponent<TrailRenderer>();
            bbTrail.startColor = bbBulletColor[GTS.indexGun];
            Color transparentTail = bbBulletColor[GTS.indexGun];
            transparentTail.a = 0.5f; // deixa o rabo transparente
            bbTrail.endColor = transparentTail;
            //change material albedo to red color

            //applyphysics
            rb.velocity = randomDirection.normalized * velocity;
            rb.mass = bbsAmmoMass[GTS.indexGun];
            bbsHopUpComponent.BackspinDrag = hopUp[GTS.indexGun];
            //applyphysics
            Destroy(bb, 5f);
        }

        fireCooldown[GTS.indexGun] = 1f / fireRate[GTS.indexGun];
    }

    public void ReLoad()
    {
        if (GTS.indexGun != -1)
        {
            if (extraCartuchos[GTS.indexGun] > 0)
            {
                int ammoNeeded = maxAmmo[GTS.indexGun] - currentAmmo[GTS.indexGun];
                if (extraCartuchos[GTS.indexGun] >= ammoNeeded)
                {
                    currentAmmo[GTS.indexGun] = maxAmmo[GTS.indexGun];
                    extraCartuchos[GTS.indexGun] -= ammoNeeded;
                }
                else
                {
                    currentAmmo[GTS.indexGun] += extraCartuchos[GTS.indexGun];
                    extraCartuchos[GTS.indexGun] = 0;
                }
            }
        }
    }

}