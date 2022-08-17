using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] CharacterController2D controller; // Reference to the CharacterController2D script via object assigned in Inspector

    float horizontalMove = 0f; // Variable to set horizontal move speed and direction

    public float runSpeed = 40f; // Variable to set horizontal move speed

    bool jump = false; // Variable that returns whether player is jumping or not

    Animator myAnimator; // Reference to this object's animator

    Rigidbody2D myRigidbody; // Reference to this object's Rigidbody2D

    [SerializeField] AudioClip footstepSound; // Reference to footstep sound, currently unused but don't want to risk deleting it

    AudioSource myAudioSource; // Reference to AudioSource component of this object

    Ghost ghost; // Reference to Ghost script

    float pickupTimer = 0f; // Timer to prevent multiple pickups of revive charges

    SoundManagement soundManagement; // Reference to SoundManagement script

    DialogueManager dialogueManager; // Reference to DialogueManager script

    float landTimer = 0f; // Timer that starts when player is on the ground to prevent multiple land sounds from playing at once
    float startTimer = .2f; // Timer that exists solely to prevent a land sound when the level starts

    [SerializeField] DialogueTrigger cutsceneObject;


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        myRigidbody = GetComponent<Rigidbody2D>(); // Assign Rigidbody2D reference to a variable by getting this object's component
        myAnimator = GetComponent<Animator>(); // Assign Animator reference to a variable by getting this object's component
        myAudioSource = GetComponent<AudioSource>(); // Assign AudioSource reference to a variable by getting this object's component
        ghost = GetComponent<Ghost>(); // Assign Ghost reference to a variable by getting this object's component
        soundManagement = GetComponent<SoundManagement>(); // Assign SoundManagement reference to a variable by getting this object's component
        float startTimer = .2f; // The value where the start timer begins to prevent landing sound from playing
    }

    // Update is called once per frame
    void Update()
    {
        if (!cutsceneObject.GetInCutscene())
        {
            startTimer -= 1 * Time.deltaTime; // Count down the start timer at the same rate regardless of framerate
            pickupTimer -= 1 * Time.deltaTime; // Count down the pickup timer at the same rate regardless of framerate
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // Get the speed and the direction of horizontal movement based on the Horizontal axis

            if (Input.GetButtonDown("Jump") && !GetComponent<Ghost>().ReturnGhost()) // If player presses Jump input
            {
                GetComponent<SoundManagement>().PlayRandomJumpSound(); // Play a random jump sound via the method in the SoundManagement script
                jump = true; // Set the jump variable to true
            }

            if (controller.ReturnMGrounded()) // If player is on the ground based on returned variable in CharacterController2D script
            {
                if (landTimer > 0 && startTimer < 0 && !ghost.ReturnGhost()) // If the land timer is greater than 0 and the start timer is less than 0 and the player is not in ghost form based on variable in Ghost script
                {
                    soundManagement.PlayRandomLandSound(); // Play a random landing sound via the method in the SoundManagement script
                }
                landTimer = 0; // Set the land timer to 0
                myAnimator.SetBool("isFalling", false); // Set the isFalling animator bool to false
                myAnimator.SetBool("isJumping", false); // Set the isJumping animator bool to false
                if (myRigidbody.velocity.x != 0) // If the rigidbody of this object currently has horizontal movement
                {
                    myAnimator.SetBool("isRunning", true); // Set the isRunning animator bool to true

                }


            }
            else if (!controller.ReturnMGrounded()) // Otherwise, if the player is not on the ground
            {
                landTimer += 1 * Time.deltaTime; // Tick up the landing timer at the same rate regardless of framerate
                if (myRigidbody.velocity.y > 0) // If the rigidbody of this object has upward movement
                {

                    myAnimator.SetBool("isJumping", true); // Set the isJumping animator bool to true
                    myAnimator.SetBool("isRunning", false); // Set the isRunning animator bool to false
                }
                else if (myRigidbody.velocity.y < 0) // Otherwise, if the rigidbody of this object has downward movement
                {
                    myAnimator.SetBool("isFalling", true); // Set the isFalling animator bool to true
                    myAnimator.SetBool("isRunning", false); // Set the isRunning animator bool to true
                }

            }

            if (myRigidbody.velocity.x > -0.4f && myRigidbody.velocity.x < 0.4f && horizontalMove == 0) // If the rigidbody of this object has inperceptible movement
            {
                myAnimator.SetBool("isRunning", false); // Don't play the running animation, because the object is not moving enough

            }
        }
        
        
        

        
    }

    private void FixedUpdate()
    {
        if (!cutsceneObject.GetInCutscene())
        {
            if (!GetComponent<Ghost>().ReturnGhost()) // If the player is not a ghost
            {
                controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump); // Move the player based on the CharacterController2D's Move method, using the value of move speed and direction, saying "false" to crouching parameter, and the bool dictating whether the player is jumping
                jump = false; // Set the jump variable to false, and I don't remember why I would do this
            }
        }
        
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     // This ought to be a method   
        if (!ghost.ReturnGhost() && collision.gameObject.GetComponent<Crucifix>() && pickupTimer <= 0f) // If player is not ghost and the other object that this object is colliding with contains the Crucifix component and the pickuped timer is 0 or less
        {
            soundManagement.PlayRandomCrucifixSound(); // Play a random crucifix pickup sound via on the method in the SoundManagement script
            pickupTimer = .2f; // Set the pickup timer to 
            Crucifix otherObject = collision.gameObject.GetComponent<Crucifix>(); // Get the other object's Crucifix component
            ghost.AddReviveCharge(); // Add to the player's revive charges via the method in the Ghost script
            Instantiate(otherObject.ReturnPickupFX(), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity); // Instantiate a pickup FX object at the location of the player, even though it probaby ought to be spawned at the other object
            
        }
    }
}
