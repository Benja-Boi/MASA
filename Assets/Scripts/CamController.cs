using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform Dud;
    public Vector3 offset;
    

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Dud.position+offset;
    }
}
