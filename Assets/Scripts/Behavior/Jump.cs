using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    GameManager gameManager; // is used to track game status

    [SerializeField] LayerMask ground; // platform
    [SerializeField] float jumpForce = 20.0f; // power for jump

    Rigidbody2D objectRigidbody; // is used to add force
    BoxCollider2D objectCollider; // is used to detect ground


    //-----EVENT FUNCTIONS----

    void Start()
    {
        gameManager = GameManager.Instance;

        objectRigidbody = GetComponent<Rigidbody2D>();
        objectCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.IsGameActive)
        {
            JumpFromTheGround();
        }
    }


    //-----MOVEMENT FUNCTIONS----

    void JumpFromTheGround()
    {
        if (IsGrounded())
        {
            objectRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(objectCollider.bounds.center, objectCollider.bounds.size, 0, Vector2.down, 0.1f, ground);
    }
}
