using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Death death; // Reference to Death script

    // Start is called before the first frame update
    void Start()
    {
        death = FindObjectOfType<Death>(); // Find the object in the scene that contains the Death script
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // If the player presses the R key
        {
            ReloadLevel(); // Restart the scene
        }
    }

    IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(death.ReturnDeathDelaySeconds()); // Wait for the amount of time assigned in the Death script (via Inspector)
        ReloadLevel(); // Reload the level after the time
    }

    public void DelayedReloadScene()
    {
        StartCoroutine(ReloadDelay()); // Run the ReloadDelay coroutine
        
    }

    public void ReloadLevel()
    {
        int thisSceneIndex = SceneManager.GetActiveScene().buildIndex; // Assign the current scene's index to a variable
        SceneManager.LoadScene(thisSceneIndex); // Reload the current scene via its assigned index
    }

    public void LoadNextLevel()
    {
        int thisSceneIndex = SceneManager.GetActiveScene().buildIndex; // Assign the current scene's index to a variable
        SceneManager.LoadScene(thisSceneIndex + 1); // Reload the next scene via its assigned index + 1
    }
}
