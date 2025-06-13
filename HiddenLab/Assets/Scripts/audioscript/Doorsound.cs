using UnityEngine;

public class Doorsound : MonoBehaviour
{
    public AudioClip openSound;
    public AudioClip closeSound;

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

    public void PlayOpenSound()
    {
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }

    public void PlayCloseSound()
    {
        if (audioSource != null && closeSound != null)
        {
            audioSource.PlayOneShot(closeSound);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayOpenSound();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayCloseSound();
        }
    }
}