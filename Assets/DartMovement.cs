using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMovement : MonoBehaviour
{
    bool isShot = false;
    public bool IsShot { get { return isShot; } set { isShot = value; } }

    public float airTime = 0.75f;
    public float minSpeed = .1f;
    public float speed;
    float timeShotFired;


    void Start()
    {
        timeShotFired = Time.time;
    }

    void Update()
    {
        if (isShot && speed >= minSpeed)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            speed *= SpeedReducerFunction(Time.time - timeShotFired);
            if (speed < minSpeed)
            {
                Destroy(this);
            }
        }
    }

    float SpeedReducerFunction(float x)
    {
        float airTimeScalar = 1 / airTime;
        return Mathf.Max((-airTimeScalar * Mathf.Pow(x, 6)) + 1, 0);
    }



}
