using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbitUsingCoroutines : MonoBehaviour
{
    public enum ActivityStates
    {
        Wandering,
        Escaping,
        Sleeping,
        Tranquilized
    }
    public ActivityStates currentState = ActivityStates.Wandering;
    public Transform player;

    [Range(0.1f, 5f)]
    public float jumpProbabilty = 1f;
    public float hopSpeed = 10f;
    public float alaramRadius = 4f;

    private bool isJumping = false;
    public Vector2 moveDir;
    private Animator animator;

    Coroutine routine = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<DudController>().transform;
    }

    void Update()
    {
        UpdateCurrentState();
        if (routine == null)
        {
            Debug.Log("routine == null");
            switch (currentState)
            {
                case ActivityStates.Wandering:
                    StopAllCoroutines();
                    routine = StartCoroutine(Wander());
                    break;
                case ActivityStates.Escaping:
                    StopAllCoroutines();
                    routine = StartCoroutine(Escape());
                    break;
                case ActivityStates.Sleeping:
                    StopAllCoroutines();
                    routine = StartCoroutine(Sleep());
                    break;
                default:
                    break;
            }
        }

        transform.localScale = new Vector3(Mathf.Sign(moveDir.x), 1, 1);
    }

    IEnumerator Jump()
    {
        animator.SetBool("isRunning", true);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("Jumping");
            transform.position += (Vector3)PickMoveDir() * hopSpeed * Time.deltaTime;
        }
    }

    void StopJumping()
    {
        animator.SetBool("isRunning", false);
        Debug.Log("stop jump");
        StopAllCoroutines();
        routine = null;
    }

    bool IsSpontaneouslyJumping()
    {
        return Random.Range(0f, 1f) < jumpProbabilty * 0.1;
    }

    Vector2 PickMoveDir()
    {
        Vector2 dir;
        if (currentState == ActivityStates.Escaping)
        {
            dir = -(player.position - transform.position);
        }
        else if (currentState == ActivityStates.Wandering)
        {
            dir = Random.insideUnitCircle;
        }
        else
        {
            dir = transform.position;
        }
        dir.Normalize();
        return dir;
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
        }
    }

    IEnumerator Wander()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            if (IsSpontaneouslyJumping())
            {
                StartCoroutine(Jump());
                break;
            }
        }
        routine = null;
    }

    IEnumerator Escape()
    {
        while (true)
        {
            yield return null;
            StartCoroutine(Jump());
            break;
        }
        routine = null;
    }

    IEnumerator Sleep()
    {
        yield return null;
    }
}
