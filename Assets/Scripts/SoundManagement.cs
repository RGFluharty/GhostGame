using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    [SerializeField] AudioClip[] footstepSounds; // A reference to the array of random footstep sounds assigned in the Inspector
    [SerializeField] AudioClip[] jumpSounds; // A reference to the array of random jump sounds assigned in the Inspector
    [SerializeField] AudioClip[] landSounds; // A reference to the array of random landing sounds assigned in the Inspector
    [SerializeField] AudioClip[] ghostSounds; // A reference to the array of random ghost sounds assigned in the Inspector -- Pretty sure I only used one
    [SerializeField] AudioClip[] reviveSounds; // A reference to the array of random revive sounds assigned in the Inspector -- Pretty sure I only used one
    [SerializeField] AudioClip[] deathSounds; // A reference to the array of random death sounds assigned in the Inspector -- Pretty sure I only used one
    [SerializeField] AudioClip[] crucifixSounds; // A reference to the array of random crucifix pickup sounds assigned in the Inspector -- Pretty sure I only used one
    AudioSource myAudioSource; // Reference to the AudioSource script


    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>(); // Assign the AudioSouce component of this object to a variable
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRandomFootstepSound()
    {
        AudioClip randomFootstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)]; // Get a random integer from 0 to the amount of footstep sounds in the array
        myAudioSource.PlayOneShot(randomFootstepSound); // Play the sound assigned to that value in the array
    }

    public void PlayRandomJumpSound()
    {
        AudioClip randomJumpSound = jumpSounds[Random.Range(0, jumpSounds.Length)];  // Get a random integer from 0 to the amount of jump sounds in the array
        myAudioSource.PlayOneShot(randomJumpSound); // Play the sound assigned to that value in the array
    }

    public void PlayRandomLandSound()
    {
        AudioClip randomLandSound = landSounds[Random.Range(0, landSounds.Length)]; // Get a random integer from 0 to the amount of landing sounds in the array
        myAudioSource.PlayOneShot(randomLandSound); // Play the sound assigned to that value in the array
    }

    public void PlayRandomGhostSound()
    {
        AudioClip randomGhostSound = ghostSounds[Random.Range(0, ghostSounds.Length)]; // Get a random integer from 0 to the amount of ghost sounds in the array
        myAudioSource.PlayOneShot(randomGhostSound); // Play the sound assigned to that value in the array
    }

    public void PlayRandomReviveSound()
    {
        AudioClip randomReviveSound = reviveSounds[Random.Range(0, reviveSounds.Length)]; // Get a random integer from 0 to the amount of revive sounds in the array
        myAudioSource.PlayOneShot(randomReviveSound); // Play the sound assigned to that value in the array
    }

    public void PlayRandomDeathSound()
    {
        AudioClip randomDeathSound = deathSounds[Random.Range(0, deathSounds.Length)]; // Get a random integer from 0 to the amount of death sounds in the array
        myAudioSource.PlayOneShot(randomDeathSound); // Play the sound assigned to that value in the array
    }

    public void PlayRandomCrucifixSound()
    {
        AudioClip randomCrucifixSound = crucifixSounds[Random.Range(0, crucifixSounds.Length)]; // Get a random integer from 0 to the amount of crucifix pickup sounds in the array
        myAudioSource.PlayOneShot(randomCrucifixSound); // Play the sound assigned to that value in the array
    }

}
