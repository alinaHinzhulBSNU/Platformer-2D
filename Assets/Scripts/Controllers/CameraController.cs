using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap; // location
    [SerializeField] GameObject objectToFollow; // moving object (character, car, etc.)

    // Camera position bounds
    float minCameraX;
    float maxCameraX;

    float minCameraY;
    float maxCameraY;


    //-----EVENT FUNCTIONS----
    
    private void Start()
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

        // Camera initial state
        SetUpCameraPosition(minCameraX, minCameraY, transform.position.z);
    }

    void LateUpdate()
    {
        FollowObject();
    }


    //-----FOLLOW OBJECT FUNCTIONS----

    void FollowObject()
    {
        if (objectToFollow != null)
        {
            Vector3 objectPosition = objectToFollow.transform.position;
            Vector3 oldCameraPosition = transform.position;

            bool isInXBounds = objectPosition.x > minCameraX && objectPosition.x < maxCameraX;
            bool isInYBounds = objectPosition.y > minCameraY;

            float x = isInXBounds? objectPosition.x : oldCameraPosition.x;
            float y = isInYBounds ? objectPosition.y : oldCameraPosition.y;
            float z = oldCameraPosition.z;

            SetUpCameraPosition(x, y, z);
        }
    }

    void SetUpCameraPosition(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }
}
