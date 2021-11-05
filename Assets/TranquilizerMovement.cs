using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranquilizerMovement : MonoBehaviour
{
    [SerializeField]
    float rotationRadius = 3f;

    Vector2 pivot;
    Vector2 mousePos;
    Vector2 dir;

    void Start()
    {
        
    }

    void Update()
    {
        pivot = transform.parent.position + new Vector3(0f, .75f, 0f);
        FaceTowardsMousePosition();
        RotateAroundCenter();
    }

    void FaceTowardsMousePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = dir;
    }

    void RotateAroundCenter()
    {
        transform.position = pivot + dir.normalized * rotationRadius;
    }
}
