using UnityEngine;

public class Doorsound : MonoBehaviour
{
    public AudioClip DoorSound;
    
    private AudioSource audioSource;
    private bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = DoorSound;

    }
    void OnTriggerEnter(Collider other) // Detect when player enters trigger zone
    {
        if (other.CompareTag("Player")) // Make sure it's the player
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(DoorSound); // Play sound once
            }

            isOpen = !isOpen; // Toggle door state
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
           
        }
        isOpen = !isOpen;

    }
}
