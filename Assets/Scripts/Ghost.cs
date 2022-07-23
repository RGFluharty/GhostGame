using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ghost : MonoBehaviour
{
    [SerializeField] bool isGhost = false; // Whether or not player is in ghost form
    float storedGravity = 3f; // The gravity scale of the human form is stored to this variable when turned to ghost, then applied back to human form
    [SerializeField] float ghostMoveSpeed = 40f; // Movement speed for ghost form
    float ghostHorizontalMove; // Movement direction for ghost form
    

    // References cached via code
    Death death; // Reference to Death script
    SoundManagement soundManagement; // Reference to SoundManagement script
    CharacterController2D controller; // Reference to CharacterController2D script

    // References cached via object in Inspector
    [SerializeField] GameObject ghostTransitionFX; // Effect object that instantiates when transforming to ghost
    [SerializeField] GameObject humanTransitionFX; // Effect object that instantiates when transforming to human
    [SerializeField] GameObject ceilingCheck; // The ceiling check object used by the character controller. Disabled when turned to ghost, enabled when turned to human.
    [SerializeField] GameObject groundCheck; // The ground check object used by the character controller. Disabled when turned to ghost, enabled when turned to human.
    [SerializeField] GameObject spotlightChild; // The spotlight object to show ghost when inside a wall. Enabled when in ghost form and touching ground layer.

    [SerializeField] int reviveCharges = 0; // Amount that player can manually turn back into human. Increased by collecting cruxifixes. -1 when player manually transforms to human.

    [SerializeField] GameObject reviveChargesTextObject; // The HUD element above the player that displays how many manual revive charges remain.

    


    // Start is called before the first frame update
    void Start()
    {
        CacheReferences();
        spotlightChild.SetActive(false);
        
    }

    public void CacheReferences()
    {
        controller = GetComponent<CharacterController2D>();
        death = GetComponent<Death>();
        soundManagement = GetComponent<SoundManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3") && !isGhost && !GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("LightColliders"))) // If press button and isn't ghost and isn't touching a light source...
        {
            TurnToGhost(); // Turn player to ghost
        }
        else if (Input.GetButtonDown("Fire3") && !GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")) && reviveCharges > 0) // otherwise, if press button and isn't touching a light source and has a manual revive charge...
        {
            reviveCharges--; // Remove a manual revive charge
            TurnBackHuman(); // Turn player back to human
        }

        if (isGhost && GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))) // If player is ghost and is touching the ground layer (floors, ceilings, walls)...
        {
            spotlightChild.SetActive(true); // Turn on the spotlight game object, allowing player to see ghost inside of walls
        }

        if (reviveCharges > 0) // If the player has at least one revive charge...
        {
            reviveChargesTextObject.SetActive(true); // Turn on the revive charge HUD element above player
        }
        else
        {
            reviveChargesTextObject.SetActive(false); // or turn it off if the player doesn't have a revive charge
        }

        reviveChargesTextObject.GetComponent<TextMeshPro>().text = "x" + reviveCharges.ToString(); // Update the revive charge HUD element to show number of revive charges
        ghostHorizontalMove = Input.GetAxisRaw("Horizontal") * ghostMoveSpeed; // Set the move direction to -1 or +1 based on Horizontal axis input and apply move speed
    }

    private void FixedUpdate()
    {
        if (isGhost)
        {
            controller.Move(ghostHorizontalMove * Time.fixedDeltaTime, false, false); // Give the character controller script the move speed and direction, false for crouch, false for jump
        }

    }

    private void TurnBackHuman()
    {
        soundManagement.PlayRandomReviveSound(); // Play revive sound effect
        GameObject thisEffect = Instantiate<GameObject>(humanTransitionFX, gameObject.transform); // Instantiate revive effect
        ceilingCheck.SetActive(true); // Reactivate ceiling check object for player controller
        groundCheck.SetActive(true); // Reactivate ground check object for player controller
        StartCoroutine(DelayAndDelete(thisEffect)); // Start the coroutine that will delete the effect after its played
        isGhost = false; // Set variable back to false, no longer ghost
        GetComponent<CapsuleCollider2D>().enabled = enabled; // Reapply human capsule collider (body)
        GetComponent<BoxCollider2D>().enabled = enabled; // Reapply human box collider (head)
        GetComponent<Rigidbody2D>().gravityScale = storedGravity; // Reapply human gravity (set in Character's Inspector)
        GetComponent<Animator>().SetBool("isGhost", false); // Set isGhost animation state to false

    }

    private void TurnToGhost()
    {
        soundManagement.PlayRandomGhostSound(); // Play ghost transformation sound effect
        GameObject thisEffect = Instantiate<GameObject>(ghostTransitionFX, gameObject.transform); // Instantiate ghost transformation effect
        ceilingCheck.SetActive(false); // Disable ceiling check object for player controller
        groundCheck.SetActive(false); // Disable ground check object for player controller
        StartCoroutine(DelayAndDelete(thisEffect)); // Start the coroutine that will delete the effect after its played
        isGhost = true; // Set variable to true, is now ghost
        GetComponent<CapsuleCollider2D>().enabled = !enabled; // Disable human capsule collider (body)
        GetComponent<BoxCollider2D>().enabled = !enabled; // Disable human box collider (head)
        storedGravity = GetComponent<Rigidbody2D>().gravityScale; // Store human gravity scale to be reapplied on revive (set in Character's Inspector)
        GetComponent<Animator>().SetBool("isGhost", true); // Set isGhost animation state to true
        GetComponent<Rigidbody2D>().gravityScale = 0f; // Remove all gravity from ghost, locking him to horizontal movement
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f); // Stop all vertical velocity to prevent vertical movement on transform
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isGhost && collision.gameObject.layer == 6) // If player is ghost and touches layer 6 (light source)
        {
            TurnBackHuman(); // Turn player back to human
        }

        if (isGhost && collision.gameObject.layer == 10) // If player is ghost and touches layer 10 (boundary)
        {
            death.Die(); // Kill player
        }

        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isGhost && collision.gameObject.layer == 8) // If the player is a ghost and it collides with an object that's assigned to Layer 8
        {
            spotlightChild.SetActive(false); // Activate the spotlight object
        }
    }

    public bool ReturnGhost()
    {
        return isGhost; // Return whether or not the player is in ghost form
    }

    IEnumerator DelayAndDelete(GameObject effect)
    {
        yield return new WaitForSeconds(1f); // Delay this method
        Destroy(effect); // Destroy the effect object
    }

    public void AddReviveCharge()
    {
        reviveCharges++; // Add 1 to the revive charges total
    }

}
