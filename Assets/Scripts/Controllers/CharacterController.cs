using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    GameManager gameManager; // is used to change game state

    // Animation
    float horizontalInput;
    Rigidbody2D characterRigidbody;
    Animator characterAnimator;

    // Detect movement
    Vector3 previousPosition;
    bool isCharacterMoving = false;


    //-----EVENT FUNCTIONS----

    void Start()
    {
        gameManager = GameManager.Instance;

        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Detect movement
        horizontalInput = Input.GetAxis("Horizontal");
        DetectMovement();

        // Setup animation
        if (gameManager.IsGameActive) SetupAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject item = collision.gameObject;

        switch (item.tag)
        {
            case Tags.CHERRY:
                gameManager.CollectCherry(item);
                break;
            case Tags.GEM:
                gameManager.CollectGem(item);
                break;
            case Tags.ENEMY:
                gameManager.Die(gameObject);
                break;
            case Tags.HOUSE:
                gameManager.GetHome(gameObject);
                break;
            default:
                break;
        }
    }


    //-----ANIMATION----

    void SetupAnimation()
    {
        bool isRunning = horizontalInput != 0;
        bool isJumping = Math.Abs(characterRigidbody.velocity.y) > 0.1f;

        // Setup idle animatiom
        if (!isRunning && !isJumping && characterAnimator.GetInteger("state") != (int)AnimationStates.IDLE)
        {
            characterAnimator.SetInteger("state", (int)AnimationStates.IDLE);
        }

        // Set up running animation
        if (isRunning && characterAnimator.GetInteger("state") != (int)AnimationStates.RUNNING)
        {
            characterAnimator.SetInteger("state", (int)AnimationStates.RUNNING);
        }

        // Set up jump animation
        if (isJumping)
        {
            characterAnimator.SetInteger("state", (int)AnimationStates.JUMPING);
        }
    }


    //-----DETECT MOVEMENT----

    void DetectMovement()
    {
        isCharacterMoving = previousPosition != transform.position;
        previousPosition = transform.position;
    }


    //-----PROPERTIES----

    public bool IsCharacterMoving
    {
        get
        {
            return isCharacterMoving;
        }
    }
}
