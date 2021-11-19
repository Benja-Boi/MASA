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

    public float knockBackForce = 20f;
    public Slider slider;
    bool isCharging = false;
    DudController player;
    ToolMovement movementScript;

    private void Start()
    {
        player = FindObjectOfType<DudController>();
        movementScript = GetComponent<ToolMovement>();
    }


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
            slider.gameObject.SetActive(true);
            isCharging = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            ShootDart();
        }
    }

    void ShootDart()
    {
        DartMovement newDart = Instantiate<DartMovement>(dart, firingPoint.position, transform.rotation);
        newDart.speed = firingSpeedModifier * maxFiringSpeed;
        newDart.IsShot = true;
        player.KnockBack(-movementScript.dir.normalized, knockBackForce * firingSpeedModifier);
        ResetParameters();
    }

    void ResetParameters()
    {
        isCharging = false;
        firingSpeedModifier = 0;
        slider.gameObject.SetActive(false);
    }
}
