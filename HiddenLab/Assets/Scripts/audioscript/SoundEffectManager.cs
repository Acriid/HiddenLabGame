using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

// Manages playing sound effects 
//https://www.youtube.com/watch?v=rAX_r0yBwzQ
public class SoundEffectManager : MonoBehaviour
{
    
    private static SoundEffectManager instance; // makes sure there's only one copy of a class in your entire game, and that you can access it from anywhere.
    private static AudioSource audioSource; // AudioSource for playing sounds

    private static SoundEffectLibrary soundEffectLibrary;  // Reference to the sound effect library



    // Ensures only one instance persists across scenes
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // Cache component references
            audioSource = GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();

            // Make this object persist between scene loads
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy duplicate instances
            Destroy(gameObject);
        }
    }

    // Static method to play a sound effect by name
    public static void Play(string soundName, bool loop)
    {
        AudioClip clip = soundEffectLibrary.GetRandomClip(soundName);
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Setup volume listener on startup
    void Start()
    {
        
    }

    
    
    
    

    // Optional: logic to run every frame (not used here)
    void Update()
    {

    }

    public static AudioClip GetClip(string soundName)
    {
        return soundEffectLibrary.GetRandomClip(soundName);
    }

    internal static void Play(string v)
    {
        throw new NotImplementedException();
    }

    internal static void Stop(string v)
    {
        throw new NotImplementedException();
    }
}