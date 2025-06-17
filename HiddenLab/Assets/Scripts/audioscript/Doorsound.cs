using UnityEngine;

public class Doorsound : MonoBehaviour
{
    public AudioClip DoorSound;
   

    private AudioSource audioSource;
    private Doors doorScript;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on this door!");
        }

        // Find and connect with the Doors script
        doorScript = GetComponent<Doors>();
        if (doorScript == null)
        {
            Debug.LogWarning("Doors script is missing on this GameObject!");
        }
    }

    public void PlayDoorSound()
    {
       audioSource.Play();
       
    }

   
    void Update()
    {
       
    }
}