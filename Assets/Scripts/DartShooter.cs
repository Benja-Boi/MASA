using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartShooter : MonoBehaviour
{
    [SerializeField]
    DartMovement dart;
    [SerializeField]
    Transform firingPoint;
    [SerializeField]
    float firingSpeed = 10f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootDart();
        }
    }

    void ShootDart()
    {
        DartMovement newDart = Instantiate<DartMovement>(dart, firingPoint.position, transform.rotation);
        newDart.speed = firingSpeed;
        newDart.IsShot = true;
    }
}
