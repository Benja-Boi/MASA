using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rbit : MonoBehaviour
{
    public enum States
    {
        Jumping,
        Wandering,
        Escaping,
        Sleeping
    }
    [SerializeField]
    States currentState;
    Stack<States> stateStack = new Stack<States>();

    [Range(0f, 0.005f)]
    public float jumpProbabilty = 0.001f;
    public float hopSpeed = 10f;
    public float alaramRadius = 1f;
    public float dist;
    
    private Vector2 moveDir = Vector2.zero;
    private Animator animator;
    public float sleepTimeLeft;
    private float sleepTimer;
    private Transform player;
    private ParticleSystem ps;

    private void Awake()
    {
        stateStack.Push(States.Wandering);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<DudController>().transform;
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Stop();
    }

    void Update()
    {
        currentState = stateStack.Peek();
        dist = Vector2.Distance(transform.position, player.position);
        switch (currentState)
        {
            case States.Wandering:
                Wander();
                break;
            case States.Escaping:
                Escape();
                break;
            case States.Sleeping:
                Sleep();
                break;
            case States.Jumping:
                Jump();
                break;
            default:
                break;
        }      

        transform.localScale = new Vector3(Mathf.Sign(moveDir.x), 1, 1);
    }

    void Wander()
    {
        if (Vector2.Distance(transform.position, player.position) < alaramRadius)
        {
            stateStack.Push(States.Escaping);
        }
        else if (IsSpontaneouslyJumping())
        {
            StartJumping();
        }

    }

    void Escape()
    {
        if (!(Vector2.Distance(transform.position, player.position) < alaramRadius) && stateStack.Peek() == States.Escaping)
        {
            stateStack.Pop();
        }
        else
        {
            stateStack.Push(States.Escaping);
            StartJumping();
        }
    }

    void Sleep()
    {
        sleepTimeLeft -= Time.deltaTime;
        if (sleepTimeLeft < 0)
        {
            if (stateStack.Peek() == States.Sleeping)
            {
                stateStack.Pop();
                ps.Stop();
            }

        }
    }

    void Jump()
    {
        
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            StopJumping();
        }
        else
        {
            dist = Vector2.Distance(transform.position, player.position);
            float urgency = Mathf.Max(3 - (dist / 3), 1);
            transform.position += (Vector3)moveDir * hopSpeed * urgency * Time.deltaTime;
        }
    }

    void StartJumping()
    {
        stateStack.Push(States.Jumping);
        animator.SetBool("isRunning", true);
        GetMoveDir();
    }

    void StopJumping()
    {
        animator.SetBool("isRunning", false);
        if (stateStack.Peek() == States.Jumping)
        {
            stateStack.Pop();
        }
    }

    bool IsSpontaneouslyJumping()
    {
        float rand = Random.Range(0f, 1f);

        return rand < jumpProbabilty;
    }

    void GetMoveDir()
    {
        if (currentState == States.Escaping)
        {
            moveDir = - (player.position - transform.position);
        }
        else
        {
            moveDir = Random.insideUnitCircle;
        }
        moveDir.Normalize();
    }

    public void Tranquilize(float sleepTime)
    {
        stateStack.Push(States.Sleeping);
        sleepTimeLeft = sleepTime;
        ps.Play();
    }
}
