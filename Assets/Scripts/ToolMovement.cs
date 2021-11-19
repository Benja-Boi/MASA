using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolMovement : MonoBehaviour
{
    [SerializeField]
    float rotationRadius = 3f;
  

    public Transform pivot;
    private SpriteRenderer sr;

    Vector2 mousePos;
    public Vector2 dir;

    void Start()
    {
       sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateSortingOrder();
        FaceTowardsMousePosition();
        RotateAroundCenter();
        MaybeFlipSide();
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
    }

    void UpdateSortingOrder()
    {
        if (mousePos.y > pivot.position.y)
        {
            sr.sortingOrder = -5;
        } else
        {
            sr.sortingOrder = 5;
        }
    }

    void MaybeFlipSide()
    {
        if (mousePos.x > pivot.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
