using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // Import the TextMesh Pro namespace

public class SliderHopUpController : MonoBehaviour
{
    public Slider slider;
    public Slider sliderSpringForce;
    public Slider sliderFireRateForce;
    private BBsGunFire bBsGunFire;
    private GunTypeState gunTypeState;
    private float maxHopUp = 0.1f;
    private float maxFireJouleForce = 5f;
    private float maxFireRate = 100f;
    private float scrollSpeed = 0.025f; 
    public Toggle tgAutofire;

    void Start()
    {
        gunTypeState = GetComponent<GunTypeState>();
        bBsGunFire = GetComponent<BBsGunFire>();
    }

    void Update()
    {
        if (gunTypeState.indexGun != -1)
        {
            slider.value = bBsGunFire.hopUp[gunTypeState.indexGun];
            tgAutofire.isOn = bBsGunFire.itsHaveAutoFire[gunTypeState.indexGun];
            sliderSpringForce.value = bBsGunFire.energyInJoules[gunTypeState.indexGun];
            sliderFireRateForce.value = bBsGunFire.fireRate[gunTypeState.indexGun];
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f && Input.GetKey(KeyCode.LeftAlt))
            {
                bBsGunFire.fireRate[gunTypeState.indexGun] = Mathf.Clamp(bBsGunFire.fireRate[gunTypeState.indexGun] + scroll * scrollSpeed * 500f, 0f, maxFireRate);
                sliderFireRateForce.value = bBsGunFire.fireRate[gunTypeState.indexGun];
            }
            else if (scroll != 0f && Input.GetKey(KeyCode.LeftShift))
            {
                bBsGunFire.energyInJoules[gunTypeState.indexGun] = Mathf.Clamp(bBsGunFire.energyInJoules[gunTypeState.indexGun] + scroll * scrollSpeed * 33f, 0f, maxFireJouleForce);
                sliderSpringForce.value = bBsGunFire.energyInJoules[gunTypeState.indexGun];
            }
            else if (scroll != 0f)
            {
                bBsGunFire.hopUp[gunTypeState.indexGun] = Mathf.Clamp(bBsGunFire.hopUp[gunTypeState.indexGun] + scroll * scrollSpeed, 0f, maxHopUp);
                slider.value = bBsGunFire.hopUp[gunTypeState.indexGun];

            }
        }
    }
}
