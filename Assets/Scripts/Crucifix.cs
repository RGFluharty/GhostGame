using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crucifix : MonoBehaviour
{
    [SerializeField] GameObject pickupFX; // Reference to pickup FX object, assigned in Inspector

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ghost>()) // If the object that enters the trigger of this object contains a Ghost component
        {
            Ghost otherObject = collision.gameObject.GetComponent<Ghost>(); // Assign the other object to a variable
            if (!otherObject.ReturnGhost()) // Check that other object's ghost component to see if the ghost bool is true or false, and if it's false
            {
                Destroy(gameObject); // Destroy this object
            }
        }
        
    }

    public GameObject ReturnPickupFX()
    {
        return pickupFX; // Return the object of the pickupFX reference assigned in the Inspector
    }
}
