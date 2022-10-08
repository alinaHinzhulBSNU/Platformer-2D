using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Animator animator; // is used to start animation
    BoxCollider2D itemCollider;


    //-----EVENT FUNCTIONS----
    
    void Start()
    {
        animator = GetComponent<Animator>();
        itemCollider = GetComponent<BoxCollider2D>();
    }


    //-----SHOW ANIMATION AND DESTROY----

    // N.B. Destroying object script is attached to animation state in order to destroy object AFTER animation finish
    public void ShowAnimationAndDestroy()
    {
        itemCollider.enabled = false; // can`t be collect during feedback animation
        animator.SetTrigger("collected");
    }
}
