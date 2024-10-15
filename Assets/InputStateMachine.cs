using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;

public class InputStateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isShooting;
    public bool isWalking;
    public bool isRuning;
    public bool isAiming;
    public bool isReloading;
    private float reloadTime = 3f;

    private bool reRunningCd;

    public bool unableToShooting;
    public float blenderReload;
    public bool isJumping;
    [Range(-1, 1)]
    public float moveX;
    [Range(-1, 1)]
    public float moveZ;

    public float imprecisionInput;

    public bool canReload;
    private decalInstarnt decalInst;
    public BBsGunFire BGF;
    void Start()
    {
        BGF = GetComponent<BBsGunFire>();
        decalInst = GetComponent<decalInstarnt>();
    }

    // Update is called once per frame
    void Update()
    {
        handleInput();
        imprecisionInput = Mathf.Clamp((System.Math.Abs(moveX) + System.Math.Abs(moveZ)) / 1, 0, 1); //for guns precision

        if (isRuning || isReloading)
        {
            unableToShooting = true;
        }

        if (unableToShooting)
        {

        }
        else
        {
            reloadingTimer();
        }
    }
    void handleInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        //imprecisionInput = (moveX + moveZ).normalized 
        if (moveX != 0 || moveZ != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        if (isWalking)
        {
            if (!reRunningCd)
            {
                if (Input.GetButton("Fire3"))
                {
                    isRuning = true;
                }
            }
        }
        if ((Input.GetButtonUp("Fire3") || !isWalking || isReloading) && isRuning)
        {
            isRuning = false;
            StartCoroutine(stopRunningCDFire());
            StartCoroutine(reRuningCdCalc());

        }
        if (moveZ < 0 && isRuning)
        {
            isRuning = false;
            StartCoroutine(stopRunningCDFire());
            StartCoroutine(reRuningCdCalc());

        }
    }
    void reloadingTimer()
    {
        if (canReload)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isReloading) // Ensure it's not already reloading
            {
                blenderReload = Random.Range(0, 2);
                StartCoroutine(Reload());
            }
        }else{
            //out of extra ammunation sound
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime); // Wait for reload duration
        isReloading = false;
        BGF.ReLoad();
        unableToShooting = false;
        decalInst.decalInst();
        Debug.Log("Reload complete.");
    }

    IEnumerator stopRunningCDFire()
    {
        Debug.Log("stop fire...");
        unableToShooting = true;
        yield return new WaitForSeconds(0.4f); // Wait for stop running transition duration
        if (!isReloading)
        {
            unableToShooting = false;
            if (isRuning)
            {
                unableToShooting = true;
            }
        }
    }
    IEnumerator reRuningCdCalc()
    {
        reRunningCd = true;
        yield return new WaitForSeconds(0.5f); // Wait for stop running transition duration
        reRunningCd = false;

    }
}
