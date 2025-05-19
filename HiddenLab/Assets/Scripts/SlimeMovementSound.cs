using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the Audio Source
    public AudioClip movementSound; // movement sound clip


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
       audioSource = GetComponent<AudioSource>(); 
        audioSource.Play();
    }

    // Update is called once per frame
    private void Sound()
    {
        bool isMoving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0;

        if (isMoving && !audioSource.isPlaying) // Check if the movement is detected and sound is not already playing
        {
            audioSource.clip = movementSound;
            audioSource.Play();
        }

    }
}
