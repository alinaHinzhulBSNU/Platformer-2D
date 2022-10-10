using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap; // location
    [SerializeField] GameObject character; // character

    CharacterController characterController; // controller is used to detect movement

    // Camera position bounds
    float minCameraX;
    float maxCameraX;

    float minCameraY;
    float maxCameraY;


    //-----EVENT FUNCTIONS----
    
    private void Start()
    {
        // Get character controller
        characterController = character.GetComponent<CharacterController>();

        // Setup bounds
        SetupBounds();

        // Camera initial state
        SetUpCameraPosition(minCameraX, minCameraY, transform.position.z);
    }

    void LateUpdate()
    {
        if (characterController.IsCharacterMoving)
        {
            FollowCharacter();
        }   
    }


    //-----SETUP----

    void SetupBounds()
    {
        // Get tilemap size
        Vector3Int bottomLeftCell = tilemap.origin;
        Vector3Int topRightCell = tilemap.origin + tilemap.size;

        // Get camera size
        Camera camera = gameObject.GetComponent<Camera>();

        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        // Calc camera bounds
        minCameraX = bottomLeftCell.x + cameraWidth / 2;
        maxCameraX = topRightCell.x - cameraWidth / 2;

        minCameraY = bottomLeftCell.y + cameraHeight / 2;
        maxCameraY = topRightCell.y - cameraHeight / 2;
    }

    void SetUpCameraPosition(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }


    //-----FOLLOW CHARACTER----

    void FollowCharacter()
    {
        if (character != null)
        {
            Vector3 characterPosition = character.transform.position;
            Vector3 oldCameraPosition = transform.position;

            bool isInXBounds = characterPosition.x > minCameraX && characterPosition.x < maxCameraX;
            bool isInYBounds = characterPosition.y > minCameraY;

            float x = isInXBounds? characterPosition.x : oldCameraPosition.x;
            float y = isInYBounds ? characterPosition.y : oldCameraPosition.y;
            float z = oldCameraPosition.z;

            SetUpCameraPosition(x, y, z);
        }
    }
}
