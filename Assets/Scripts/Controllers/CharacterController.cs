using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] LayerMask ground;

    Rigidbody2D characterRigidbody;
    BoxCollider2D characterCollider;
    Animator characterAnimator;
    SpriteRenderer characterSpriteRenderer;

    bool faceToRight = true;


    //-----EVENT FUNCTIONS----

    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<BoxCollider2D>();
        characterAnimator = GetComponent<Animator>();
        characterSpriteRenderer = GetComponent<SpriteRenderer>();

        characterAnimator.SetInteger("state", (int)AnimationStates.IDLE);
    }

    void FixedUpdate()
    {
        if (gameManager.IsGameActive) Move();
    }

    private void Update()
    {
        if (!gameManager.IsGameActive) return;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Set jump animation
        if (Math.Abs(characterRigidbody.velocity.y) > 0.1f)
        {
            characterAnimator.SetInteger("state", (int)AnimationStates.JUMPING);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject item = collision.gameObject;

        if (item.CompareTag(TAGS.CHERRY))
        {
            gameManager.CollectCherry(item);
        }
        else if (item.CompareTag(TAGS.GEM))
        {
            gameManager.CollectGem(item);
        }
        else if (item.CompareTag(TAGS.ENEMY))
        {
            gameManager.GameOver(gameObject);
        }
        else if (item.CompareTag(TAGS.HOUSE))
        {
            gameManager.LevelUp();
        }
    }


    //-----CHARACTER BEHAVIOUR----

    void TurnAround()
    {
        characterSpriteRenderer.flipX = faceToRight;
        faceToRight = !faceToRight;
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Face the direction of movement
        bool turnLeft = horizontalInput < 0 && faceToRight;
        bool turnRight = horizontalInput > 0 && !faceToRight;

        if (turnLeft || turnRight)
        {
            TurnAround();
        }

        // Move
        bool canGoRight = transform.position.x < GLOBALS.rightCharacterBound && horizontalInput > 0;
        bool canGoLeft = transform.position.x > GLOBALS.leftCharacterBound && horizontalInput < 0;

        if (canGoLeft || canGoRight)
        {
            transform.Translate(Vector2.right * GLOBALS.movementSpeed * Time.deltaTime * horizontalInput);
            characterAnimator.SetInteger("state", (int)AnimationStates.RUNNING);
        }
        else
        {
            characterAnimator.SetInteger("state", (int)AnimationStates.IDLE);
        }
    }

    void Jump()
    {
        bool isGrounded = Physics2D.BoxCast(characterCollider.bounds.center, characterCollider.bounds.size, 0, Vector2.down, 0.1f, ground);

        if (isGrounded)
        {
            characterRigidbody.AddForce(new Vector2(0, GLOBALS.jumpForce), ForceMode2D.Impulse);
        }
    }
}
