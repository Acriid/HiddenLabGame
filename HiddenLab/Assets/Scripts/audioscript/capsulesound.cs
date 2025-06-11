using UnityEngine;

public class CapsuleSound : MonoBehaviour
{
    // Allows audio source and player to be assigned in the Inspector
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform player;

    // Maximum distance where sound remains audible
    [SerializeField] private float triggerRadius = 5f;

    private void Start()
    {
        // Ensure the audio source is assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Set up audio properties
        ConfigureAudio();
        Debug.Log("Distance to player: " + Vector3.Distance(transform.position, player.position));
    }

    private void Update()
    {
        // Adjust sound volume based on how close the player is
        AdjustVolumeBasedOnDistance();
    }

    private void ConfigureAudio()
    {
        if (audioSource != null)
        {
            audioSource.spatialBlend = 1f; // Enables 3D sound effects
            audioSource.loop = true; // Keeps the sound playing
            audioSource.Play(); // Starts playback, volume is controlled dynamically
        }
    }

    private void AdjustVolumeBasedOnDistance()
    {
        // Ensure necessary components exist
        if (audioSource == null || player == null) return;

        // Calculate the distance between the player and the object
        float distance = Vector3.Distance(transform.position, player.position);

        // Smoothly adjust volume based on proximity
        audioSource.volume = (distance < triggerRadius) ? Mathf.Clamp01(1 - (distance / triggerRadius)) : 0f;
    }
}