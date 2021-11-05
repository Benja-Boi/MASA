using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranquilizerMovement : MonoBehaviour
{
    [SerializeField]
    float rotationRadius = 3f;
    public Transform pivot;

    Vector2 mousePos;
    Vector2 dir;

    void Start()
    {
        
    }

    void Update()
    {
        FaceTowardsMousePosition();
        RotateAroundCenter();
    }

    void FaceTowardsMousePosition()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = new Vector2(mousePos.x - pivot.position.x, mousePos.y - pivot.position.y);
        transform.up = dir;
    }

    void RotateAroundCenter()
    {
        transform.position = (Vector2)pivot.position + dir.normalized * rotationRadius;
        Debug.Log(dir.normalized);
    }
}
