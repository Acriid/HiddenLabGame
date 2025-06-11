using UnityEngine;

public class Doorsound : MonoBehaviour
{
    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;
    private bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) // Detect when player enters trigger zone
    {
        if (other.CompareTag("Player")) // Make sure it's the player
        {
            // Toggle door state
            isOpen = !isOpen;

            // Play respective sound based on door state
            if (isOpen)
            {
                audioSource.PlayOneShot(openSound);
            }
            else
            {
                audioSource.PlayOneShot(closeSound);
            }
        }
    }
}