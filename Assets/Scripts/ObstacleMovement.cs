using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // Speed at which the object travels

    [SerializeField] float pauseTimeInSeconds = 2f; // How long the object pauses when it reaches the destination before it begins to travel again
    float pauseTimer = 2f; // Timer that ticks down when object reaches destination

    Rigidbody2D myRigidbody; // Reference to object's Rigidbody2D

    [SerializeField] GameObject startPoint; // Reference to start point object (assigned in Inspector)
    [SerializeField] GameObject endPoint; // Reference to end point object (assigned in Inspector)

    bool reachedDestination; // Bool that returns true when object reaches start or end point
    bool reverseDirection; // Bool that changes switches the destination to start/end point after original destination is reached

    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>(); // Assign Rigidbody2D reference
        // myRigidbody.transform.position = startPoint.transform.position; I don't remember why this is here
        pauseTimer = pauseTimeInSeconds; // Ensure pause timer is same as Inspector's pauseTimeInSeconds
    }

    // Update is called once per frame
    void Update()
    {
        float step = moveSpeed * Time.deltaTime; // Make sure movement speed remains consistent regardless of framerate, assign the equation as "step"

        if (!reachedDestination && !reverseDirection) // If the object has reached its destination and reverseDirection is false
        {
            myRigidbody.transform.position = Vector2.MoveTowards(myRigidbody.transform.position, endPoint.transform.position, step); // Move towards the endPoint object at step speed
        }

        else if (!reachedDestination && reverseDirection) // Else if the object has reached its destination and reverseDirection is true
        {
            myRigidbody.transform.position = Vector2.MoveTowards(myRigidbody.transform.position, startPoint.transform.position, step); // Move towards the startPoint object at step speed
        }

        if (reachedDestination) // If object has reached destination
        {
            pauseTimer -= 1 * Time.deltaTime; // Start ticking down the pauseTimer (same speed regardless of framerate)
            if (pauseTimer <= 0) // Once the pause timer reaches zero
            {
                reachedDestination = false; // The object has no longer reached its destination
                pauseTimer = pauseTimeInSeconds; // And the pause timer is set back to its start value
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.GetComponent<EndPoint>() && !reachedDestination) // If object enters EndPoint object collider and reachedDestination is false
        {
            reachedDestination = true; // The object has reached its destination
            reverseDirection = true; // And it needs to reverse its direction
        }
        if (collision.gameObject.GetComponent<StartPoint>() && !reachedDestination) // If the object enters Startpoint object collider and reachedDestination is false
        {
            reachedDestination = true; // The object has reached its destination
            reverseDirection = false; // It no longer needs to reverse from its original direction
        }
    }

}
