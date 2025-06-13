using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// Manages playing sound effects and adjusting their volume through a UI slider
public class SoundEffectManager : MonoBehaviour
{
    // Singleton instance for global access
    private static SoundEffectManager instance;

    // AudioSource for playing sounds
    private static AudioSource audioSource;

    // Reference to the sound effect library
    private static SoundEffectLibrary soundEffectLibrary;

    // UI slider to control sound effect volume
    [SerializeField] private Slider sfxSlider;

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
        sfxSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    // Static method to set volume directly
    public static void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    // Called whenever the slider value changes
    public void OnValueChanged()
    {
        SetVolume(sfxSlider.value);
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