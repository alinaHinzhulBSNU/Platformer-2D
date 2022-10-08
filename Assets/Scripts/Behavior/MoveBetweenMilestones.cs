using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenMilestones : MonoBehaviour
{
    // Milestones
    [SerializeField] List<GameObject> milestones;
    int currentMilestoneIndex = 0;

    // Face the movement direction
    SpriteRenderer objectSpriteRenderer;
    bool faceToLeft = false;

    // Movement speed
    [SerializeField] float speed = 2.0f;


    //-----EVENT FUNCTIONS----

    private void Start()
    {
        objectSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (milestones.Count > 0)
        {
            // Define current milestone
            Vector2 milestonePosition = milestones[currentMilestoneIndex].transform.position;
            Vector2 obljectPosition = transform.position;

            if (Vector2.Distance(milestonePosition, obljectPosition) < 0.1f)
            {
                currentMilestoneIndex = currentMilestoneIndex + 1 >= milestones.Count ? 0 : currentMilestoneIndex + 1; // goal
                faceToLeft = !faceToLeft; // face
            }

            // Move
            transform.position = Vector2.MoveTowards(obljectPosition, milestonePosition, Time.deltaTime * speed);

            // Face the direction of movement
            objectSpriteRenderer.flipX = faceToLeft;
        }
    }
}
