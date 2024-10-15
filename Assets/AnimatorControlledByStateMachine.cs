using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControlledByStateMachine : MonoBehaviour
{
    private InputStateMachine ISM;
    private GunTypeState GTS;

    [Header("0 = Rifle, 1 = Shotgun, 2 = Pistol, 3 = Pistol 1911")]
    public Animator[] animators;

    void Start()
    {
        ISM = GetComponent<InputStateMachine>();
        GTS = GetComponent<GunTypeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GTS.indexGun >= 0 && GTS.indexGun < animators.Length)
        {
            Animator currentAnimator = animators[GTS.indexGun];
            if (ISM.isShooting)
            {
                currentAnimator.Play("shoot");
            }
            currentAnimator.SetBool("isReloading", ISM.isReloading);
            currentAnimator.SetBool("isWalking", ISM.isWalking);
            currentAnimator.SetBool("isRuning", ISM.isRuning);
            currentAnimator.SetFloat("randReload", ISM.blenderReload);
        }
        else if (GTS.indexGun == -1)
        {
         
        }
    }
}
