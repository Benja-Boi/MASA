using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudController : MonoBehaviour
{
    public Animator animator;
    public GameObject dustParticles;
    public float moveSpeed = 3f;
    private Vector2 velocity;
   
    
    private void Start()
    {
        animator.SetBool("isIdle", true);
    }
    void Update()
    {
        //get inputs and calculate direction
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //multiply direction by speed
        velocity = direction.normalized * moveSpeed * Time.deltaTime;

        //move the player
        transform.Translate(velocity);
        

        //Animation
        if (direction.x != 0 || direction.y !=0)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", true);
            dustParticles.SetActive(true);
        } else if (direction.x == 0 || direction.y == 0)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
            dustParticles.SetActive(false);
        }
        
        
        
    }
}
