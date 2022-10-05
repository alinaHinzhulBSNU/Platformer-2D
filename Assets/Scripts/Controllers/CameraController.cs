using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject character;

    void FixedUpdate()
    {
        FollowCharacter();
    }

    void FollowCharacter()
    {
        if(character != null)
        {
            // Direction of character movement
            float horizontalInput = Input.GetAxis("Horizontal");

            // Character position
            float characterX = character.transform.position.x;
            float characterY = character.transform.position.y;

            // Camera default state WITHOUT movement (X axis)
            float cameraX = transform.position.x;
            float cameraY = characterY; // ALWAYS follow character Y axis
            float cameraZ = transform.position.z;

            // Can camera move left or right
            bool moveCameraLeft = false;
            bool moveCameraRight = false;

            if (horizontalInput > 0)
            {
                moveCameraRight = cameraX < characterX && cameraX < GLOBALS.rightCameraBound;
            }
            else if (horizontalInput < 0)
            {
                moveCameraLeft = cameraX > characterX && cameraX > GLOBALS.leftCameraBound;
            }

            // Set X for camera
            if (moveCameraRight || moveCameraLeft)
            {
                cameraX = characterX;
            }

            // Move camera
            transform.position = new Vector3(cameraX, cameraY, cameraZ);
        }
    }
}
