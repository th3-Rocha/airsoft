using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    public Camera MainCam;                  // Reference to the camera
    public float aimFOV = 30f;              // The FOV when aiming
    public float normalFOV = 60f;           // The default FOV
    public float transitionSpeed = 5f;      // Speed of the FOV transition
    private bool isAiming = false;          // Boolean to check if aiming
    public GameObject aimPrefab;            // The prefab that shows the aim point
    public Transform rayOutPutTransform;    // The transform from where the raycast originates
    public FPSCharacterControllerrr FCC;
    private BBsGunFire bBsGunFire;
    private GunTypeState GTS;
    private float OriginalMouseSens;
    public float maxRayDistance = 100f;     // Max distance for the raycast
    public LayerMask ignoreLayers;          // LayerMask to ignore specific layers in the raycast
    public float minScale = 0.1f;           // Minimum scale for the aimPrefab
    public float maxScale = 1f;             // Maximum scale for the aimPrefab
    public float scalingFactor = 0.05f;     // Factor to scale the ball based on distance

    void Start()
    {
        FCC = GetComponent<FPSCharacterControllerrr>();
        OriginalMouseSens =  FCC.mouseSensitivity;
        bBsGunFire = GetComponent<BBsGunFire>();
        GTS = GetComponent<GunTypeState>();

        if (MainCam != null)
        {
            MainCam.fieldOfView = normalFOV;
        }
        ignoreLayers = ~LayerMask.GetMask("Decal");
    }

    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        if (isAiming && GTS.indexGun != -1)
        {
            rayOutPutTransform = bBsGunFire.BBsOutputTransform[GTS.indexGun];
            Ray ray = new Ray(rayOutPutTransform.position, rayOutPutTransform.forward); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRayDistance, ignoreLayers))
            {
                aimPrefab.transform.position = hit.point;
                aimPrefab.transform.LookAt(rayOutPutTransform);
                float distance = Vector3.Distance(rayOutPutTransform.position, hit.point);
                float scaleValue = Mathf.Clamp(distance * scalingFactor, minScale, maxScale);
                aimPrefab.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

                aimPrefab.SetActive(true);
            }
            else
            {
                aimPrefab.SetActive(false);
            }
        }
        else
        {
            aimPrefab.SetActive(false);
        }
        FCC.mouseSensitivity =  isAiming ? OriginalMouseSens/2 : OriginalMouseSens;
        float targetFOV = isAiming ? aimFOV : normalFOV;
        MainCam.fieldOfView = Mathf.Lerp(MainCam.fieldOfView, targetFOV, transitionSpeed * Time.deltaTime);
    }
}
