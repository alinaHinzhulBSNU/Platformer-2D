using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControlledMovement : MonoBehaviour
{
    GameManager gameManager; // is used to track game status
    SpriteRenderer objectSpriteRenderer; // is used to flip the sprite

    bool faceToRight = true; // face direction

    [SerializeField] float movementSpeed = 5.0f; // speed of movement
    [SerializeField] Tilemap tilemap; // location for movement

    float rightObjectBound; // right location bound
    float leftObjectBound; // left location bound
    float boundOffset = 0.5f;


    //-----EVENT FUNCTIONS----

    void Start()
    {
        gameManager = GameManager.Instance;

        objectSpriteRenderer = GetComponent<SpriteRenderer>();

        // Calc movement bounds
        Vector3Int bottomLeftCell = tilemap.origin;
        Vector3Int topRightCell = tilemap.origin + tilemap.size;

        leftObjectBound = bottomLeftCell.x + boundOffset;
        rightObjectBound = topRightCell.x - boundOffset;
    }

    void FixedUpdate()
    {
        if (gameManager.IsGameActive) Move();
    }


    //-----MOVEMENT FUNCTIONS----

    void TurnAround()
    {
        objectSpriteRenderer.flipX = faceToRight;
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
        bool canGoRight = transform.position.x < rightObjectBound && horizontalInput > 0;
        bool canGoLeft = transform.position.x > leftObjectBound && horizontalInput < 0;

        if (canGoLeft || canGoRight)
        {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime * horizontalInput);
        }
    }
}
