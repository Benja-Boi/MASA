using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetShooter : MonoBehaviour
{
    [SerializeField]
    FlyingNet net;
    [SerializeField]
    Transform firingPoint;
    [SerializeField]
    float maxFiringSpeed = 0f;
    [Range(0f, 1f)]
    [SerializeField]
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
        } else if(Input.GetButtonUp("Fire1"))
        {
            ShootNet();
        }
    }

    void ShootNet()
    {
        isCharging = false;
        FlyingNet newNet = Instantiate<FlyingNet>(net, firingPoint.position, transform.rotation);
        newNet.speed = firingSpeedModifier * maxFiringSpeed;
        newNet.IsShot = true;
        firingSpeedModifier = 0;
    }

}
