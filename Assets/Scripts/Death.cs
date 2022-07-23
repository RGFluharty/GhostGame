using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    Ghost ghost; // Reference to Ghost script
    [SerializeField] GameObject deathFX; // Reference to deathFX game object, assigned in Inspector
    [SerializeField] float deathDelayInSeconds = 2f; // The amount of time between a player dying and the level reloading, assigned in Inspector
    LevelManager levelManager; // Reference to LevelManager script
    SoundManagement soundManagement; // Reference to SoundManagement script

    private void Start()
    {
        ghost = GetComponent<Ghost>(); // Assign Ghost script reference to a variable to access its code by getting this object's Ghost component
        levelManager = FindObjectOfType<LevelManager>(); // Assign the LevelManager script by finding the object in the scene that contains the LevelManager component
        soundManagement = GetComponent<SoundManagement>(); // Assign SoundManagement script reference to a variable to access its code by getting this object's SoundManagement component
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9) // If this object collides with an object's collider that's assigned to Layer 9
        {
            Die(); // Run the Die method (kill the player)
        }
    }

    public void Die()
    {
        Instantiate(deathFX, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity); // Create an instance of the DeathFX object at this object's location
        levelManager.DelayedReloadScene(); // Run the DelayedReloadScene method from the LevelManager script
        Destroy(gameObject); // Destroy this object
    }

    public float ReturnDeathDelaySeconds()
    {
        return deathDelayInSeconds; // Return the assigned death delay seconds
    }

    private void OnDestroy()
    {
        
    }
}
