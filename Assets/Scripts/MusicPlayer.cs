using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is code I stole and I kind of don't understand it. It keeps the music player object alive after a scene change.

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance; 

    public static MusicPlayer Instance { get { return instance; } } 

    AudioSource myAudioSource;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    
}
