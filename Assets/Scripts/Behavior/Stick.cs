using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    string desiredTag = Tags.CHARACTER; // Object to stick


    //-----EVENT FUNCTIONS----

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject item = collision.gameObject;

        if (item.CompareTag(desiredTag))
        {
            item.transform.SetParent(transform); // stick
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject item = collision.gameObject;

        if (item.CompareTag(desiredTag))
        {
            item.transform.SetParent(null); // unstick
        }
    }
}
