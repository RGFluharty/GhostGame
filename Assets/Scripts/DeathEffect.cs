using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I am fairly certain I did not end up using this script

public class DeathEffect : MonoBehaviour
{

    [SerializeField] AudioClip deathSound1;
    [SerializeField] AudioClip deathSound2;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.PlayOneShot(deathSound1);
        myAudioSource.PlayOneShot(deathSound2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
