using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rbit : MonoBehaviour
{
    public enum ActivityStates
    {
        Wandering,
        Escaping,
        Sleeping
    }
    public ActivityStates currentState;
    public Transform player;

    [Range(0f, 0.005f)]
    public float jumpProbabilty = 0.001f;
    public float hopSpeed = 10f;
    public float alaramRadius = 4f;

    private bool isJumping = false;
    private bool isEscaping = false;
    public Vector2 moveDir;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<DudController>().transform;
    }

    void Update()
    {
        UpdateCurrentState();

        switch (currentState)
        {
            case ActivityStates.Wandering:
                Wander();
                break;
            case ActivityStates.Escaping:
                Escape();
                break;
            case ActivityStates.Sleeping:
                break;
            default:
                break;
        }

        if (isJumping)
        {
            transform.position += (Vector3)moveDir * hopSpeed * Time.deltaTime;
        }

        transform.localScale = new Vector3(Mathf.Sign(moveDir.x), 1, 1);
    }

    void Wander()
    {
        if (IsSpontaneouslyJumping())
        {
            Jump();
        }
    }

    void Escape()
    {
        isEscaping = true;
        Jump();
    }


    void Jump()
    {
        animator.SetBool("isRunning", true);
        PickMoveDir();
        isJumping = true;
    }

    void StopJumping()
    {
        animator.SetBool("isRunning", false);
        isJumping = false;
    }

    bool IsSpontaneouslyJumping()
    {
        float rand = Random.Range(0f, 1f);

        return rand < jumpProbabilty;
    }

    void PickMoveDir()
    {
        if (isEscaping)
        {
            moveDir = - (player.position - transform.position);
        }
        else
        {
            moveDir = Random.insideUnitCircle;
        }
        moveDir.Normalize();
    }

    void UpdateCurrentState()
    {
        if (Vector2.Distance(transform.position, player.position) < alaramRadius)
        {

            currentState = ActivityStates.Escaping;
        }
        else
        {
            currentState = ActivityStates.Wandering;
            isEscaping = false;
        }
    }
}
