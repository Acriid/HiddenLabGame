using UnityEngine;
using UnityEngine.Audio;


public class Capsulesound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Attach an AudioSource to the capsule

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioSource.clip); // Play sound locally
        }
    }
}