using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] LevelManager levelManager; // Reference to the level manager object, assigned in Inspector
    [SerializeField] Ghost playerObject; // Reference to the player object, assigned in Inspector

    // Start is called before the first frame update
    void Start()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() && !playerObject.ReturnGhost()) // If the object that enters this object's trigger contains CharacterController2D script and the ReturnGhost() method returns false
        {
            levelManager.LoadNextLevel(); // Load the next level
        }
    }

}
