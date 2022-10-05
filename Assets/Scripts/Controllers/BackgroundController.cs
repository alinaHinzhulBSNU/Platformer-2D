using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;

    void FixedUpdate()
    {
        FollowCamera();
    }

    void FollowCamera()
    {
        if(mainCamera != null)
        {
            Vector3 backgroundOffset = new Vector3(0, 0, -mainCamera.transform.position.z);
            transform.position = mainCamera.transform.position + backgroundOffset;
        }
    }
}
