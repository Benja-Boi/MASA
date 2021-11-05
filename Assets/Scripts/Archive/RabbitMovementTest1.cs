using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    private enum ActivityStates
    {
        Wandering,
        Running,
        Sleeping
    }

    public Animator animator;
    [Range (1f, 100f)]
    public float hopSpeed = 10f;
    public Vector2 minMaxHopCooldown = new Vector2(1f, 4f);
    public Vector2 minMaxHopRadius = new Vector2(1f, 4f);
    private ActivityStates currentState;
    
    private Vector2 targetPoint;
    private float lastJumpStartTime;
    private float hopCooldown;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentState = ActivityStates.Wandering;
        ChooseRandomTarget();
        RandomizeHopCooldown();
        lastJumpStartTime = Time.timeSinceLevelLoad;

    }

    // Update is called once per frame
    void Update()
    {   

        //if (isRunning)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, targetPoint, hopSpeed * Time.deltaTime);
        //}

        switch (currentState)
        {
            case ActivityStates.Wandering:
                Wander();
                break;
            case ActivityStates.Running:
                break;
            case ActivityStates.Sleeping:
                break;
            default:
                break;
        }
    }

    void Wander()
    {
        if (Time.timeSinceLevelLoad - lastJumpStartTime >= hopCooldown)
        {
            ChooseRandomTarget();
            RandomizeHopCooldown();
            lastJumpStartTime = Time.timeSinceLevelLoad;

        }
        if(Vector2.Distance(transform.position, targetPoint) > 0.2f)
        {
            animator.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, hopSpeed * Time.deltaTime);
            //transform.position += (Vector3)((targetPoint - (Vector2) transform.position)* hopSpeed * Time.deltaTime);========== EASEOUT
            
        } else
        {
            animator.SetBool("isRunning", false);
        }
          
    }

    void Run()
    {

    }

    void ChooseRandomTarget()
    {
        targetPoint = (Random.insideUnitCircle.normalized * Random.Range(minMaxHopRadius.x, minMaxHopRadius.y)) + (Vector2) transform.position;
    }

    void RandomizeHopCooldown()
    {
        hopCooldown = Random.Range(minMaxHopCooldown.x, minMaxHopCooldown.y);
    }

    //void Jump()
    //{
    //    // choose targetDir
    //    isRunning = true;
    //}

    //void StopJump()
    //{
    //    isRunning = false;
    //}
}
