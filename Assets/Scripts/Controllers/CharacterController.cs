using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    GameManager gameManager; // is used to change game state


    //-----EVENT FUNCTIONS----

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        if (gameManager.IsGameActive) SetUpAnimation();
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

    void SetUpAnimation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Rigidbody2D characterRigidbody = GetComponent<Rigidbody2D>();
        Animator characterAnimator = GetComponent<Animator>();

        bool isJumping = Math.Abs(characterRigidbody.velocity.y) > 0.1f;
        bool isRunning = horizontalInput != 0;

        // Set up running or idle animation
        if (isRunning)
        {
            characterAnimator.SetInteger("state", (int)AnimationStates.RUNNING);
        }
        else
        {
            characterAnimator.SetInteger("state", (int)AnimationStates.IDLE);
        }

        // Set up jump animation
        if (isJumping)
        {
            characterAnimator.SetInteger("state", (int)AnimationStates.JUMPING);
        }
    }
}
