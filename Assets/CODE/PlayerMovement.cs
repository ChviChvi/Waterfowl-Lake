using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Components
    Rigidbody2D rigidBody;
    
    //Player
    float walkSpeed;
    float speedLimiter = 0.7f;
    float inputHorizontal;
    float inputVertical;
    
    // Animations and states
    Animator animator;
    string currentState;
    const string PLAYER_IDLE_SIT = "Player_Idle_Sit";
    const string PLAYER_WALK_LEFT = "Player_Walk_Left";
    const string PLAYER_WALK_RIGHT = "Player_Walk_Right";
    const string PLAYER_WALK_UP = "Player_Up";
    const string PLAYER_WALK_DOWN = "Player_Down";

    bool facingLeft = true;
    
    
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            rigidBody.velocity = new Vector2(inputHorizontal, inputVertical).normalized * Running() * 2f;

            if (inputHorizontal > 0)
            {
                ChanegAnimationState(PLAYER_WALK_LEFT);
                if (!facingLeft)
                {
                    Flip();
                }
                
            }
            else if (inputHorizontal < 0)
            {
                ChanegAnimationState(PLAYER_WALK_LEFT);
                if (facingLeft)
                {
                    Flip();
                }
            }
            else if (inputVertical > 0)
            {
                ChanegAnimationState(PLAYER_WALK_LEFT);
            }
            else if (inputVertical < 0)
            {
                ChanegAnimationState(PLAYER_WALK_LEFT);
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0,0).normalized;
            ChanegAnimationState(PLAYER_IDLE_SIT);
            //print("hello");
        }
    }
    
    //Animation state Changer
    void ChanegAnimationState(string newState)
    {
        // stop animation form interrupting itself
        if (currentState == newState) return;
        
        // Plays new animaton
        animator.Play(newState);
        
        // Update current state
        currentState = newState;


    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;

        gameObject.transform.localScale = currentScale;

        facingLeft = !facingLeft;
    }

    float Running()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (walkSpeed < 10f)
            {
                walkSpeed += 0.5f;
            }
        }
        else
        {
            walkSpeed = 2f;
        }

        return walkSpeed;
    }
    
    
}
