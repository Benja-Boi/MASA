using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DartShooter : MonoBehaviour
{
    [SerializeField]
    DartMovement dart;
    [SerializeField]
    Transform firingPoint;
    [SerializeField]
    float maxFiringSpeed = 0f;
    [Range(0f, 1f)] [SerializeField]
    float firingSpeedModifier;
    [SerializeField]
    float maxChargeTime = 1.5f;

    public Slider slider;
    bool isCharging = false;


    void Update()
    {
        slider.value = firingSpeedModifier;
        if (isCharging && firingSpeedModifier < 1)
        {
            firingSpeedModifier += (1 / maxChargeTime) * Time.deltaTime;
            firingSpeedModifier = Mathf.Min(firingSpeedModifier, 1);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            isCharging = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ShootDart();
        }
    }

    void ShootDart()
    {
        isCharging = false;
        DartMovement newDart = Instantiate<DartMovement>(dart, firingPoint.position, transform.rotation);
        newDart.speed = firingSpeedModifier * maxFiringSpeed;
        newDart.IsShot = true;
        firingSpeedModifier = 0;
    }
}
