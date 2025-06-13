using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// Manages playing sound effects 
public class SoundEffectManager : MonoBehaviour
{
    // Singleton instance for global access
    private static SoundEffectManager instance;

    // AudioSource for playing sounds
    private static AudioSource audioSource;

    // Reference to the sound effect library
    private static SoundEffectLibrary soundEffectLibrary;

    

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
    public static void Play(string soundName)
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
}